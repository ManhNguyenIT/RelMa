using RelMa.Domain.Locations;
using RelMa.Domain.Parts;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Storages;

public class StorageEntity : Entity<DefaultIdType>
{
    public StorageEntity()
    {
        Items = new HashSet<PartEntity>();
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DefaultIdType? LocationId { get; set; }
    public virtual LocationEntity? Location { get; set; }
    public virtual ICollection<PartEntity> Items { get; }
}
