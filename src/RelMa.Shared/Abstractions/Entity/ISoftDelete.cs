namespace RelMa.Shared.Abstractions.Entity;
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    string? DeletedBy { get; set; }

    public void Delete();
    void Undo();
}
