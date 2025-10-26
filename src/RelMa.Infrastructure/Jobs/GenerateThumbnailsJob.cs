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

        var status = new Dictionary<string, object>
        {
            ["status"] = ProcessingStatus.Queued,
            ["message"] = string.Empty
        };

        await cache.GetOrCreateAsync(
            key: $"status",
            param: id,
            factory: _ => Task.FromResult(status),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
            cancellationToken: context.CancellationToken
        );
        try
        {
            await imageService.GenerateThumbnailsAsync(fileName);

            status["status"] = ProcessingStatus.Completed;
            await cache.GetOrCreateAsync(
                key: $"status",
                param: id,
                factory: _ => Task.FromResult(status),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: context.CancellationToken
            );
        }
        catch(Exception ex)
        {
            status["status"] = ProcessingStatus.Failed;
            status["message"] = ex.Message;
            await cache.GetOrCreateAsync(
                key: $"status",
                param: id,
                factory: _ => Task.FromResult(status),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: context.CancellationToken
            );
            throw;
        }
    }
}
