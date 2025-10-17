namespace RelMa.Shared.Abstractions.Entity;
public interface IUserTracking<TKey>
{
    TKey? CreatedBy { get; set; }
    TKey? ModifiedBy { get; set; }
}
