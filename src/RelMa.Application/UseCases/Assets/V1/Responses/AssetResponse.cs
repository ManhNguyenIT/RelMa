using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Application.UseCases.Manufacturers.V1.Responses;

namespace RelMa.Application.UseCases.Assets.V1.Responses;

public class AssetResponse
{
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Model { get; set; }
    public required Ulid LocationId { get; set; }
    public virtual LocationResponse? Location { get; set; }
    public Ulid? ManufacturerId { get; set; }
    public virtual ManufacturerResponse? Manufacturer { get; set; }
    public string? SerialNumber { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Barcode { get; set; }
}
