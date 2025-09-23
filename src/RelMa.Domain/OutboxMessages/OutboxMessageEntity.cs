using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.OutboxMessages;

public class OutboxMessageEntity : ITenantTracking
{
    public DefaultIdType Id { get; set; }
    public required string Type { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset OccurredAt { get; set; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public string? TenantId { get; set; }

    public OutboxMessageEntity()
    {
        Id = DefaultIdType.CreateVersion7();
        OccurredAt = DateTimeOffset.Now;
    }

    public static OutboxMessageEntity Create(string type, string content)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(content);
        return new OutboxMessageEntity() { Type = type, Content = content };
    }

    public static void UpdateProcessedTime(OutboxMessageEntity entity)
    {
        entity.ProcessedAt = DateTimeOffset.Now;
    }
}
