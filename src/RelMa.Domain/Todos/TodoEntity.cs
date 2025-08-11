using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Todos;

public class TodoEntity : Entity<Ulid>
{
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public Priority Priority { get; set; }

    private readonly List<string> _labels = [];
    public IReadOnlyCollection<string> Labels => _labels.AsReadOnly();

    public void AddLabel(string label)
    {
        if (!_labels.Contains(label))
        {
            _labels.Add(label);
        }
    }

    public void RemoveLabel(string label) => _labels.Remove(label);

}
