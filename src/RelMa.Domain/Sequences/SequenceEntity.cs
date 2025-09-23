using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Sequences;

public class SequenceEntity : ITenantTracking
{
    public DateOnly SeqDate { get; set; }
    public int CurrentValue { get; set; }
    public string? TenantId { get; set; }
    public string? TableName { get; set; }
}
