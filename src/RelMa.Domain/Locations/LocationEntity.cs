using RelMa.Domain.Assets;
using RelMa.Domain.Items;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Locations;

public class LocationEntity : Entity<Ulid>
{
    public LocationEntity()
    {
        Items = new HashSet<ItemEntity>();
        Assets = new HashSet<AssetEntity>();
        Children = new HashSet<LocationEntity>();
    }
    public required string Name { get; set; }
    public Ulid? ParentId { get; set; }
    public virtual LocationEntity? Parent { get; set; }
    public virtual ICollection<ItemEntity> Items { get; }
    public virtual ICollection<AssetEntity> Assets { get; }
    public virtual ICollection<LocationEntity> Children { get; }
}
