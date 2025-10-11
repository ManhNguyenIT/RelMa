using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Services;
using System.IO.Pipelines;

namespace RelMa.Infrastructure.Services;

internal sealed class FileService(IMinioClient minioClient, IUserContext userContext) : IFileService
{
    public async Task<IEnumerable<string>> CopyFilesAsync(string folder, string[] files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = await userContext.GetFileConfig()
            ?? throw new InvalidOperationException($"File config not found.");

        var targetFolder = string.IsNullOrWhiteSpace(folder) ? string.Empty : $"{folder.TrimEnd('/')}/";

        using var semaphore = new SemaphoreSlim(10);
        var tasks = files.Select(async file =>
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                var fileName = string.Join('_', file.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                var objectName = $"{targetFolder}{fileName}";

                var copyArgs = new CopyObjectArgs()
                    .WithBucket(fileConfig.TargetBucket)
                    .WithObject(objectName)
                    .WithCopyObjectSource(new CopySourceObjectArgs()
                        .WithBucket(fileConfig.TempBucket)
                        .WithObject(file));

                await minioClient.CopyObjectAsync(copyArgs, cancellationToken);

                return objectName;
            }
            finally
            {
                semaphore.Release();
            }
        });

        return await Task.WhenAll(tasks);
    }

    public async Task DeleteTempFilesAsync(string[] files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = await userContext.GetFileConfig()
            ?? throw new InvalidOperationException($"File config not found.");

        using var semaphore = new SemaphoreSlim(10);
        var tasks = files.Select(async file =>
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                var removeArgs = new RemoveObjectArgs()
                    .WithBucket(fileConfig.TempBucket)
                    .WithObject(file);
                await minioClient.RemoveObjectAsync(removeArgs, cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
    }

    public async Task<PipeReader> DownloadFilesAsync(string[] files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = await userContext.GetFileConfig()
            ?? throw new InvalidOperationException($"File config not found.");

        var pipe = new Pipe(new PipeOptions(minimumSegmentSize: 81920));
        var writer = pipe.Writer;

        try
        {
            if (files.Length == 1)
            {
                await minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(fileConfig.TargetBucket)
                    .WithObject(files[0])
                    .WithCallbackStream(async (stream, token) =>
                    {
                        byte[] buffer = new byte[81920];
                        int bytesRead;
                        while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                        {
                            await writer.WriteAsync(buffer.AsMemory(0, bytesRead), token);
                        }
                    }), cancellationToken);
                await writer.FlushAsync(cancellationToken);
                await writer.CompleteAsync();
            }
            else
            {
                await using var zipOutputStream = new ZipOutputStream(pipe.Writer.AsStream());
                zipOutputStream.SetLevel(1);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var entry = new ZipEntry(fileName)
                    {
                        DateTime = DateTime.Now,
                        CompressionMethod = CompressionMethod.Deflated
                    };
                    await zipOutputStream.PutNextEntryAsync(entry, cancellationToken);

                    await minioClient.GetObjectAsync(new GetObjectArgs()
                        .WithBucket(fileConfig.TargetBucket)
                        .WithObject(file)
                        .WithCallbackStream(async (stream, token) =>
                        {
                            byte[] buffer = new byte[81920];
                            int bytesRead;
                            while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                            {
                                await zipOutputStream.WriteAsync(buffer.AsMemory(0, bytesRead), token);
                            }
                        }), cancellationToken);

                    zipOutputStream.CloseEntry();
                }

                zipOutputStream.Finish();
                await writer.FlushAsync(cancellationToken);
                await writer.CompleteAsync();
            }
        }
        catch (Exception ex)
        {
            await writer.CompleteAsync(ex);
            throw;
        }

        return pipe.Reader;
    }

    public async Task<IEnumerable<string>> GetFilesAsync(string? prefix = null, string? pattern = null, string[]? extensions = null, CancellationToken cancellationToken = default)
    {
        var fileConfig = await userContext.GetFileConfig()
            ?? throw new InvalidOperationException($"File config not found.");

        var listArgs = new ListObjectsArgs()
            .WithBucket(fileConfig.TargetBucket)
            .WithRecursive(true);

        if (!string.IsNullOrEmpty(prefix))
        {
            listArgs.WithPrefix(prefix.TrimEnd('/') + "/");
        }

        // Optimize extensions lookup with HashSet
        var extensionSet = extensions != null && extensions.Length > 0
            ? new HashSet<string>(extensions.Where(e => !string.IsNullOrEmpty(e)), StringComparer.OrdinalIgnoreCase)
            : null;

        var results = new List<string>();
        var tcs = new TaskCompletionSource<bool>();
        var observable = minioClient.ListObjectsAsync(listArgs, cancellationToken);

        using var subscription = observable.Subscribe(
            item =>
            {
                if (!item.IsDir && IsValidFile(item.Key, pattern, extensionSet))
                {
                    results.Add(item.Key);
                }
            },
            ex => tcs.TrySetException(ex),
            () => tcs.TrySetResult(true)
        );

        using (cancellationToken.Register(() => tcs.TrySetCanceled()))
        {
            await tcs.Task;
        }

        return results;
    }

    private static bool IsValidFile(string fileKey, string? pattern, HashSet<string>? extensions)
    {
        // Check pattern
        if (!string.IsNullOrEmpty(pattern) && !fileKey.Contains(pattern, StringComparison.OrdinalIgnoreCase))
            return false;

        // Check extensions
        if (extensions != null && extensions.Count > 0 && !extensions.Any(ext => fileKey.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            return false;

        return true;
    }

    public async Task<IEnumerable<string>> UploadFilesAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Count == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = await userContext.GetFileConfig()
            ?? throw new InvalidOperationException($"File config not found.");

        var sessionId = DefaultIdType.CreateVersion7();
        using var semaphore = new SemaphoreSlim(10);

        var tasks = files.Select(async file =>
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                if (!string.IsNullOrEmpty(fileConfig.MaxFileSize) && file.Length > ParseFileSize(fileConfig.MaxFileSize))
                    throw new ArgumentException($"File {file.FileName} exceeds size limit.");

                await using var stream = file.OpenReadStream();
                var fileName = string.Join('_', Path.GetFileName(file.FileName).Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                var objectName = $"{sessionId}/{fileName}";

                var putArgs = new PutObjectArgs()
                    .WithBucket(fileConfig.TempBucket)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(file.Length)
                    .WithContentType(file.ContentType);

                var response = await minioClient.PutObjectAsync(putArgs, cancellationToken);
                return response.ObjectName;
            }
            finally
            {
                semaphore.Release();
            }
        });

        return await Task.WhenAll(tasks);
    }

    private static long ParseFileSize(string fileSize)
    {
        fileSize = fileSize.Trim().ToUpperInvariant();

        var numberPart = new string([.. fileSize.TakeWhile(c => char.IsDigit(c) || c == '.' || c == '-')]);
        var unitPart = fileSize[numberPart.Length..];

        if (string.IsNullOrEmpty(numberPart) || string.IsNullOrEmpty(unitPart) || !double.TryParse(numberPart, out var size))
            throw new ArgumentException($"Invalid MaxFileSize format: {fileSize}. Expected format: '10KB', '1MB', '2.5GB'. Supported units: B, KB, MB, GB, TB, PB.");

        var multiplier = unitPart switch
        {
            "B" => 1L,
            "KB" => 1024L,
            "MB" => 1024L * 1024L,
            "GB" => 1024L * 1024L * 1024L,
            "TB" => 1024L * 1024L * 1024L * 1024L,
            "PB" => 1024L * 1024L * 1024L * 1024L * 1024L,
            _ => throw new ArgumentException($"Unsupported unit in MaxFileSize: {unitPart}. Supported units: B, KB, MB, GB, TB, PB.")
        };

        if (size < 0)
            throw new ArgumentException($"MaxFileSize cannot be negative: {fileSize}.");

        return (long)(size * multiplier);
    }
}
