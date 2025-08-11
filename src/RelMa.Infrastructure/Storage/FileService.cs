using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using RelMa.Application.Abstractions.Services;
using System.Collections.Concurrent;
using System.IO.Compression;

namespace RelMa.Infrastructure.Storage;

internal sealed class FileService(IMinioClient minioClient, IOptions<FileConfig> options) : IFileService
{
    public async Task<IEnumerable<string>> CopyFilesAsync(string folder, string[] files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = options.Value;

        var tasks = files.Select(async file =>
        {
            var fileName = file.Replace('/', '_');
            var objectName = string.IsNullOrWhiteSpace(folder) ? fileName : $"{folder.TrimEnd('/')}/{fileName}";

            var copyArgs = new CopyObjectArgs()
                .WithBucket(fileConfig.TargetBucket)
                .WithObject(objectName)
                .WithCopyObjectSource(new CopySourceObjectArgs()
                    .WithBucket(fileConfig.TempBucket)
                    .WithObject(file));

            await minioClient.CopyObjectAsync(copyArgs, cancellationToken);

            var statArgs = new StatObjectArgs()
                .WithBucket(fileConfig.TargetBucket)
                .WithObject(objectName);
            var objectStat = await minioClient.StatObjectAsync(statArgs, cancellationToken);

            var removeArgs = new RemoveObjectArgs()
                .WithBucket(fileConfig.TempBucket)
                .WithObject(file);

            await minioClient.RemoveObjectAsync(removeArgs, cancellationToken);

            return objectStat.ObjectName;
        });

        var results = await Task.WhenAll(tasks);
        return results;
    }


    public async Task<MemoryStream> DownloadFilesAsync(string[] files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files specified.");

        var fileConfig = options.Value;
        if (files.Length == 1)
        {
            var memoryStream = new MemoryStream();
            await minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(fileConfig.TargetBucket)
                .WithObject(files[0])
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                }), cancellationToken);

            memoryStream.Position = 0;
            return memoryStream;
        }
        else
        {
            var zipStream = new MemoryStream();

            var fileStreams = await Task.WhenAll(files.Select(async file =>
            {
                var ms = new MemoryStream();

                await minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(fileConfig.TargetBucket)
                    .WithObject(file)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(ms);
                    }));

                ms.Position = 0;
                return (FileName: Path.GetFileName(file), Stream: ms);
            }));

            using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true);
            foreach (var (fileName, fileStream) in fileStreams)
            {
                var entry = archive.CreateEntry(fileName, CompressionLevel.Fastest);

                await using var entryStream = entry.Open();
                await fileStream.CopyToAsync(entryStream, cancellationToken);
                await fileStream.DisposeAsync();
            }

            zipStream.Position = 0;
            return zipStream;
        }
    }

    public async Task<IEnumerable<string>> GetFilesAsync(string? prefix = null, string[]? extensions = null, CancellationToken cancellationToken = default)
    {
        var fileConfig = options.Value;
        var listArgs = new ListObjectsArgs()
            .WithBucket(fileConfig.TargetBucket)
            .WithRecursive(true);

        if (!string.IsNullOrEmpty(prefix))
        {
            listArgs.WithPrefix(prefix.TrimEnd('/') + "/");
        }

        var results = new ConcurrentBag<string>();
        var tcs = new TaskCompletionSource<bool>();
        var observable = minioClient.ListObjectsAsync(listArgs, cancellationToken);
        using var subscription = observable.Subscribe(
            item =>
            {
                if (!item.IsDir)
                {
                    if (extensions != null && extensions.Length != 0 && !Array.Exists(extensions, ext => item.Key.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                        return;
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

    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var fileConfig = options.Value;
        var sectionId = Ulid.NewUlid().ToString();
        await using var stream = file.OpenReadStream();
        var putArgs = new PutObjectArgs()
            .WithBucket(fileConfig.TempBucket)
            .WithObject($"{sectionId}/{file.FileName}")
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        var response = await minioClient.PutObjectAsync(putArgs, cancellationToken);

        return response.ObjectName;
    }

    public async Task<IEnumerable<string>> UploadFilesAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Count == 0)
            throw new ArgumentException("No files specified.", nameof(files));

        var fileConfig = options.Value;
        var sectionId = Ulid.NewUlid().ToString();

        var fileList = files.ToList();

        var tasks = fileList.Select(async file =>
        {
            await using var stream = file.OpenReadStream();

            var putArgs = new PutObjectArgs()
                .WithBucket(fileConfig.TempBucket)
                .WithObject($"{sectionId}/{file.FileName}")
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

            var response = await minioClient.PutObjectAsync(putArgs, cancellationToken);
            return response.ObjectName;
        });

        var results = await Task.WhenAll(tasks);
        return results;
    }

}
