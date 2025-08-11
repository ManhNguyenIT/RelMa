namespace RelMa.Shared.Abstractions.Entity;
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
    Task Handle(TEvent @event, CancellationToken cancellationToken);
}
