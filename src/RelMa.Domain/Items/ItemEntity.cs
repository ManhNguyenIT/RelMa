using RelMa.Domain.Locations;
using RelMa.Domain.Materials;
using RelMa.Domain.Parts;
using RelMa.Domain.Storages;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Items;

public class ItemEntity : Entity<Ulid>
{
    public ItemEntity()
    {
        Parts = new HashSet<PartEntity>();
    }

    public int Quantity { get; set; }
    public Ulid? StorageId { get; set; }
    public virtual StorageEntity? Storage { get; set; }
    public Ulid? LocationId { get; set; }
    public virtual LocationEntity? Location { get; set; }
    public required Ulid MaterialId { get; set; }
    public virtual MaterialEntity? Material { get; set; }
    public virtual ICollection<PartEntity> Parts { get; }
}
