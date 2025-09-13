using Quartz;

namespace RelMa.Infrastructure.BackgroundJobs;

internal sealed class ProcessSchedulerJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}
