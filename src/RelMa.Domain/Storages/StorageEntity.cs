using RelMa.Domain.Items;
using RelMa.Domain.Locations;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Storages;

public class StorageEntity : Entity<Ulid>
{
    public StorageEntity()
    {
        Items = new HashSet<ItemEntity>();
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Ulid? LocationId { get; set; }
    public virtual LocationEntity? Location { get; set; }
    public virtual ICollection<ItemEntity> Items { get; }
}
