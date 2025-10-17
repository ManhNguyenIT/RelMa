using Microsoft.AspNetCore.Http;

namespace RelMa.Application.Abstractions.Services;

public interface IImageService
{
    Task<(DefaultIdType, string)> SaveImagesAsync(
        IFormFile file,
        CancellationToken cancellationToken = default);
    Task GenerateThumbnailsAsync(
        string file,
        int[]? widths = null,
        CancellationToken cancellationToken = default);
}
