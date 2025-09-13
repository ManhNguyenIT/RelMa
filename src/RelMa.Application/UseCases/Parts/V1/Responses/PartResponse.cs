using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Application.UseCases.Materials.V1.Responses;

namespace RelMa.Application.UseCases.Parts.V1.Responses;

public class PartResponse
{
    public required DefaultIdType Id { get; set; }
    public int Quantity { get; set; }
    public DefaultIdType? LocationId { get; set; }
    public DefaultIdType? MaterialId { get; set; }
    public virtual LocationResponse? Location { get; set; }
    public virtual MaterialResponse? Material { get; set; }
}
