namespace RelMa.Application.Abstractions.Jobs;

public interface IJobScheduler
{
    public string ProcessThumbnailJob { get; }
    Task ScheduleJob(string jobName, IDictionary<string, object>? parameters = null);
    Task TriggerJob(string jobName, IDictionary<string, object>? parameters = null);
}

