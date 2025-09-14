using Quartz;
using RelMa.Application.Abstractions.Services;

namespace RelMa.Infrastructure.Jobs;

public sealed class ProcessThumbnailJob(IImageService imageService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var fileName = context.MergedJobDataMap.GetString("fileName");
        if (string.IsNullOrEmpty(fileName))
            return;
        await imageService.GenerateThumbnailsAsync(fileName);
    }
}
