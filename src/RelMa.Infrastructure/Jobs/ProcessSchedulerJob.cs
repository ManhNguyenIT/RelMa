using Quartz;

namespace RelMa.Infrastructure.Jobs;

internal sealed class ProcessSchedulerJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}
