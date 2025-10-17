namespace RelMa.Shared.Abstractions.Entity;
public interface IAuditable<TKey> : IDateTracking, IUserTracking<TKey>, ISoftDelete<TKey>
{
}
