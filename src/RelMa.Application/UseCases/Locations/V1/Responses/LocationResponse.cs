namespace RelMa.Application.UseCases.Locations.V1.Responses;

public class LocationResponse
{
    public required DefaultIdType Id { get; set; }
    public required string Name { get; set; }
    public DefaultIdType? ParentId { get; set; }
    public virtual LocationResponse? Parent { get; set; }
}
