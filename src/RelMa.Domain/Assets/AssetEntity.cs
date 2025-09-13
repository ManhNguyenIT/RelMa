using RelMa.Domain.Locations;
using RelMa.Domain.Manufacturers;
using RelMa.Domain.Requests;
using RelMa.Domain.Tasks;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Assets;

public class AssetEntity : Entity<DefaultIdType>
{
    public AssetEntity()
    {
        Tasks = new HashSet<TaskEntity>();
        Requests = new HashSet<RequestEntity>();
    }
    public required string Name { get; set; }
    public string? Area { get; set; }
    public string? Barcode { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public required DefaultIdType LocationId { get; set; }
    public DefaultIdType? ManufacturerId { get; set; }

    public virtual LocationEntity? Location { get; set; }
    public virtual ManufacturerEntity? Manufacturer { get; set; }
    public virtual ICollection<TaskEntity> Tasks { get; }
    public virtual ICollection<RequestEntity> Requests { get; }
}
