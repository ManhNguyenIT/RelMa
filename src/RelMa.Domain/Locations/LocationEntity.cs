using RelMa.Domain.Assets;
using RelMa.Domain.Parts;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Locations;

public class LocationEntity : Entity<DefaultIdType>
{
    public LocationEntity()
    {
        Items = new HashSet<PartEntity>();
        Assets = new HashSet<AssetEntity>();
        Children = new HashSet<LocationEntity>();
    }
    public required string Name { get; set; }
    public DefaultIdType? ParentId { get; set; }
    public virtual LocationEntity? Parent { get; set; }
    public virtual ICollection<PartEntity> Items { get; }
    public virtual ICollection<AssetEntity> Assets { get; }
    public virtual ICollection<LocationEntity> Children { get; }
}
