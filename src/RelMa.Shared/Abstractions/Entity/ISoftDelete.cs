namespace RelMa.Shared.Abstractions.Entity;
public interface ISoftDelete<TKey>
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    TKey? DeletedBy { get; set; }

    public void Delete();
    void Undo();
}
