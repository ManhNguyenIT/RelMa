using Cortex.Mediator.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RelMa.Shared.Exceptions;
using System.Globalization;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed class ImportAssetCommandHandler(
    IWebHostEnvironment environment,
    ILogger<ImportAssetCommandHandler> logger) : ICommandHandler<ImportAssetCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(ImportAssetCommand command, CancellationToken cancellationToken)
    {
        var folderPath = Path.Combine(environment.ContentRootPath, "assets/uploads/assets");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var ext = Path.GetExtension(command.File.FileName).ToLower(CultureInfo.CurrentCulture);
        if (!ext.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("Invalid file extension");
            throw new ArgumentException("Invalid file extension", command.File.FileName);
        }

        var id = Guid.CreateVersion7();
        var fileName = $"{id}{ext}";
        var filePath = Path.Combine(folderPath, fileName);

        try
        {
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await command.File.CopyToAsync(stream, cancellationToken);
            logger.LogInformation("Saved file: {FilePath}", filePath);



            return id;
        }
        catch (IOException ex)
        {
            logger.LogError(ex, "Failed to save file: {FileName}", fileName);
            throw new BadRequestException($"Failed to save file '{fileName}' to '{filePath}'. Reason: {ex.Message}");
        }
    }
}
