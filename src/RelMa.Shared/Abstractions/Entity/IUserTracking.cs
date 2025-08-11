namespace RelMa.Shared.Abstractions.Entity;
public interface IUserTracking
{
    string? CreatedBy { get; set; }
    string? ModifiedBy { get; set; }
}
