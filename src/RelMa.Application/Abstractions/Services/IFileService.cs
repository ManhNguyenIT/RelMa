using Microsoft.AspNetCore.Http;
using System.IO.Pipelines;

namespace RelMa.Application.Abstractions.Services;

public interface IFileService
{
    Task<IEnumerable<string>> CopyFilesAsync(string folder, string[] files, CancellationToken cancellationToken = default);
    Task DeleteTempFilesAsync(string[] files, CancellationToken cancellationToken = default);
    Task<PipeReader> DownloadFilesAsync(string[] files, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetFilesAsync(string? prefix = null, string? pattern = null, string[]? extensions = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> UploadFilesAsync(IFormFileCollection files, CancellationToken cancellationToken = default);
}