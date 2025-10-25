namespace RelMa.Application.Abstractions.Jobs;

public interface IJobScheduler
{
    Task ScheduleJob(string jobName, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
    Task TriggerJob(string jobName, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
}

