using RelMa.Domain.Locations;
using RelMa.Domain.Materials;
using RelMa.Domain.Sets;
using RelMa.Domain.Storages;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Parts;

public class PartEntity : Entity<DefaultIdType>
{
    public PartEntity()
    {
        Sets = new HashSet<SetEntity>();
    }

    public required DefaultIdType MaterialId { get; set; }
    public Status Status { get; set; }
    public int Minimum { get; set; }
    public int Quantity { get; set; }
    public int Inventory { get; set; }
    public decimal Cost { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public DefaultIdType? StorageId { get; set; }
    public virtual StorageEntity? Storage { get; set; }
    public DefaultIdType? LocationId { get; set; }
    public virtual LocationEntity? Location { get; set; }
    public virtual MaterialEntity? Material { get; set; }
    public ICollection<SetEntity> Sets { get; init; }
}
