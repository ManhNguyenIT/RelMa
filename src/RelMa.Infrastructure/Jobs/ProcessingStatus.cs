namespace RelMa.Infrastructure.Jobs;

public enum ProcessingStatus
{
    None,
    Queued,
    Processing,
    Completed,
    Failed
}
