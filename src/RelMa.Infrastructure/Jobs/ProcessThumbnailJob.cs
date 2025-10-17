using Quartz;
using RelMa.Application.Abstractions.Services;
using System.Collections.Concurrent;

namespace RelMa.Infrastructure.Jobs;

public sealed class ProcessThumbnailJob(
    IImageService imageService,
    ConcurrentDictionary<DefaultIdType, ProcessingStatus> statuses
    ) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var id = context.MergedJobDataMap.GetGuid("id");
        if (id == Guid.Empty)
            return;

        var fileName = context.MergedJobDataMap.GetString("fileName");
        if (string.IsNullOrEmpty(fileName))
            return;

        statuses[id] = ProcessingStatus.Processing;
        try
        {
            await imageService.GenerateThumbnailsAsync(fileName);
            statuses[id] = ProcessingStatus.Completed;
        }
        catch
        {
            statuses[id] = ProcessingStatus.Failed;
            throw;
        }
    }
}
