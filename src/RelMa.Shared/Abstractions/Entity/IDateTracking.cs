namespace RelMa.Shared.Abstractions.Entity;
public interface IDateTracking
{
    DateTimeOffset? CreatedAt { get; set; }
    DateTimeOffset? ModifiedAt { get; set; }
}
