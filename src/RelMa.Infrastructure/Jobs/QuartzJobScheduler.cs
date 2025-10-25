using Quartz;
using RelMa.Application.Abstractions.Jobs;

namespace RelMa.Infrastructure.Jobs;

public class QuartzJobScheduler(ISchedulerFactory schedulerFactory) : IJobScheduler
{
    public async Task ScheduleJob(string jobName, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

        var jobKey = new JobKey(jobName);

        var jobExists = await scheduler.CheckExists(jobKey, cancellationToken);
        if (!jobExists)
        {
            throw new InvalidOperationException($"Job '{jobName}' chưa được đăng ký trong Quartz.");
        }

        var jobData = new JobDataMap();
        if (parameters != null)
        {
            foreach (var kvp in parameters)
            {
                jobData.Put(kvp.Key, kvp.Value.ToString());
            }
        }

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"{jobName}Trigger", "default")
            .StartNow()
            .UsingJobData(jobData)
            .Build();

        await scheduler.ScheduleJob(trigger, cancellationToken);
    }

    public async Task TriggerJob(string jobName, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName);

        var jobExists = await scheduler.CheckExists(jobKey, cancellationToken);
        if (!jobExists)
        {
            throw new InvalidOperationException($"Job '{jobName}' chưa được đăng ký trong Quartz.");
        }

        var jobData = new JobDataMap();
        if (parameters != null)
        {
            foreach (var kvp in parameters)
            {
                jobData.Put(kvp.Key, kvp.Value.ToString());
            }
        }

        await scheduler.TriggerJob(jobKey, jobData, cancellationToken);
    }
}
