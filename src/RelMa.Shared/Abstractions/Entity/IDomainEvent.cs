using Cortex.Mediator.Notifications;

namespace RelMa.Shared.Abstractions.Entity;
public interface IDomainEvent : INotification
{
    Ulid EventId { get; init; }
}
