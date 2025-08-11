using System.ComponentModel.DataAnnotations.Schema;

namespace RelMa.Shared.Abstractions.Entity;
public abstract class AggregateRoot : IAggregateRoot
{
    [NotMapped]
    private readonly List<IDomainEvent> _events;
    protected AggregateRoot()
    {
        _events = [];
    }

    [NotMapped]
    public IReadOnlyList<IDomainEvent> Events => _events.ToArray().AsReadOnly();

    public void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}
