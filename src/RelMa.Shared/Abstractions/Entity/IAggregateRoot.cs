namespace RelMa.Shared.Abstractions.Entity;
public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> Events { get; }
    void AddEvent(IDomainEvent @event);
    void ClearEvents();
}
