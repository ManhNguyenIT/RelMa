using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Application.UseCases.Materials.V1.Responses;

namespace RelMa.Application.UseCases.Items.V1.Responses;

public class ItemResponse
{
    public required Ulid Id { get; set; }
    public int Quantity { get; set; }
    public Ulid? LocationId { get; set; }
    public Ulid? MaterialId { get; set; }
    public virtual LocationResponse? Location { get; set; }
    public virtual MaterialResponse? Material { get; set; }
}
