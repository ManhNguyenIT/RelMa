namespace RelMa.Shared.Abstractions.Entity;
public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
