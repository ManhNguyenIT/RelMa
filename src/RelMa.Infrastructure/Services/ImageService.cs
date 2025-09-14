using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RelMa.Application.Abstractions.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Globalization;
using System.Threading;

namespace RelMa.Infrastructure.Services;
internal sealed class ImageService(
    IWebHostEnvironment environment,
    ILogger<ImageService> logger) : IImageService
{
    private static readonly int[] DefaultThumbnailWidths = [32, 64, 128, 256, 512, 1024];
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];
    private static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];
    private const int MaxFileSizeBytes = 10 * 1024 * 1024;
    private const string OriginalsPath = "statics/originals";
    private const string ThumbnailsPath = "statics/thumbnails";

    private static bool IsValidImage(IFormFile file, out string? error)
    {
        error = null;
        if (file == null || file.Length == 0)
        {
            error = "File is empty or null.";
            return false;
        }
        if (file.Length > MaxFileSizeBytes)
        {
            error = $"File size exceeds {MaxFileSizeBytes / (1024 * 1024)}MB limit.";
            return false;
        }
        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.CurrentCulture);
        if (!AllowedExtensions.Contains(extension))
        {
            error = $"Invalid file extension: {extension}. Allowed: {string.Join(", ", AllowedExtensions)}.";
            return false;
        }
        if (!AllowedMimeTypes.Contains(file.ContentType))
        {
            error = $"Invalid MIME type: {file.ContentType}. Allowed: {string.Join(", ", AllowedMimeTypes)}.";
            return false;
        }
        return true;
    }

    public async Task<string> SaveImagesAsync(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        var folderPath = Path.Combine(environment.ContentRootPath, OriginalsPath);
        Directory.CreateDirectory(folderPath);

        if (!IsValidImage(file, out var error))
        {
            logger.LogWarning("Invalid image upload attempt: {Error}", error);
            throw new ArgumentException(error, nameof(file));
        }

        var ext = Path.GetExtension(file.FileName).ToLower(CultureInfo.CurrentCulture);
        var fileName = $"{Guid.CreateVersion7()}{ext}";
        var filePath = Path.Combine(folderPath, fileName);

        try
        {
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await file.CopyToAsync(stream, cancellationToken);
            logger.LogInformation("Saved image: {FilePath}", filePath);
            return fileName;
        }
        catch (IOException ex)
        {
            logger.LogError(ex, "Failed to save image: {FileName}", fileName);
            throw new ImageProcessingException(
                $"Failed to save image '{fileName}' to '{filePath}'. Reason: {ex.Message}",
                ex);
        }
    }

    public async Task GenerateThumbnailsAsync(
        string file,
        int[]? widths = null,
        CancellationToken cancellationToken = default)
    {
        widths ??= DefaultThumbnailWidths;
        var folderPath = Path.Combine(environment.ContentRootPath, ThumbnailsPath);
        Directory.CreateDirectory(folderPath);

        var originalPath = Path.Combine(environment.ContentRootPath, OriginalsPath, file);
        if (!File.Exists(originalPath))
        {
            logger.LogWarning("Original image not found: {FilePath}", originalPath);
            return;
        }

        try
        {
            using var image = await Image.LoadAsync(originalPath, cancellationToken);
            var format = GetImageFormat(file);

            foreach (var width in widths)
            {
                if (width <= 0) continue;

                var fileName = $"{Path.GetFileNameWithoutExtension(file)}_w{width}{Path.GetExtension(file)}";
                var thumbPath = Path.Combine(folderPath, fileName);

                try
                {
                    using var resizedImage = image.Clone(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, 0),
                        Mode = ResizeMode.Max
                    }));
                    await resizedImage.SaveAsync(thumbPath, format, cancellationToken);
                    logger.LogInformation("Generated thumbnail: {ThumbPath}", thumbPath);
                }
                catch (IOException ex)
                {
                    logger.LogError(ex, "Failed to generate thumbnail for {FileName} at width {Width}", file, width);
                }
            }
        }
        catch (IOException ex)
        {
            logger.LogError(ex, "Failed to process image: {FilePath}", originalPath);
        }
    }

    private static IImageEncoder GetImageFormat(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLower(CultureInfo.CurrentCulture);
        return ext switch
        {
            ".png" => new PngEncoder { CompressionLevel = PngCompressionLevel.BestSpeed },
            ".jpg" or ".jpeg" => new JpegEncoder { Quality = 75 },
            ".gif" => new GifEncoder(),
            _ => throw new ArgumentException($"Unsupported image format: {ext}")
        };
    }
}
