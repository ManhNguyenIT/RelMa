using Cortex.Mediator.Notifications;

namespace RelMa.Shared.Abstractions.Entity;

public interface IDomainEvent : INotification
{
    DefaultIdType EventId { get; init; }
}
