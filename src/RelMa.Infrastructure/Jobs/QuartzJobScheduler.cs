using Quartz;
using RelMa.Application.Abstractions.Jobs;

namespace RelMa.Infrastructure.Jobs;

public class QuartzJobScheduler(ISchedulerFactory schedulerFactory) : IJobScheduler
{
    public string ProcessThumbnailJob => nameof(ProcessThumbnailJob);

    public async Task ScheduleJob(string jobName, IDictionary<string, object>? parameters = null)
    {
        var scheduler = await schedulerFactory.GetScheduler();

        var jobKey = new JobKey(jobName);

        var jobExists = await scheduler.CheckExists(jobKey);
        if (!jobExists)
        {
            throw new InvalidOperationException($"Job '{jobName}' chưa được đăng ký trong Quartz.");
        }

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"{jobName}Trigger", "default")
            .StartNow()
            .UsingJobData([.. parameters ?? new Dictionary<string, object>()])
            .Build();

        await scheduler.ScheduleJob(trigger);
    }

    public async Task TriggerJob(string jobName, IDictionary<string, object>? parameters = null)
    {
        var scheduler = await schedulerFactory.GetScheduler();
        var jobKey = new JobKey(jobName);

        var jobExists = await scheduler.CheckExists(jobKey);
        if (!jobExists)
        {
            throw new InvalidOperationException($"Job '{jobName}' chưa được đăng ký trong Quartz.");
        }

        await scheduler.TriggerJob(jobKey, [.. parameters ?? new Dictionary<string, object>()]);
    }
}
