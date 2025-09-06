namespace RelMa.Application.UseCases.Locations.V1.Responses;

public class LocationResponse
{
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public Ulid? ParentId { get; set; }
    public virtual LocationResponse? Parent { get; set; }
}
