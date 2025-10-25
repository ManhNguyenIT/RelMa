using Microsoft.Extensions.Caching.Distributed;
using Quartz;
using RelMa.Application.Abstractions.Services;
using RelMa.Infrastructure.Extentions;

namespace RelMa.Infrastructure.Jobs;

public sealed class GenerateThumbnailsJob(
    IDistributedCache cache,
    IImageService imageService
    ) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (DefaultIdType.TryParse(context.MergedJobDataMap.GetString("id"), out var id))
            return;

        var fileName = context.MergedJobDataMap.GetString("fileName");
        if (string.IsNullOrEmpty(fileName))
            return;

        await cache.GetOrCreateAsync(
            key: $"status",
            param: id,
            factory: async token => await Task.FromResult(ProcessingStatus.Processing),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
            cancellationToken: default
        );
        try
        {
            await imageService.GenerateThumbnailsAsync(fileName);
            await cache.GetOrCreateAsync(
                key: $"status",
                param: id,
                factory: async token => await Task.FromResult(ProcessingStatus.Completed),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: default
            );
        }
        catch
        {
            await cache.GetOrCreateAsync(
                key: $"status",
                param: id,
                factory: async token => await Task.FromResult(ProcessingStatus.Failed),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: default
            );
            throw;
        }
    }
}
