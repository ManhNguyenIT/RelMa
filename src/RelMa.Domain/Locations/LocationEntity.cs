using RelMa.Domain.Assets;
using RelMa.Domain.Parts;
using RelMa.Domain.Storages;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Locations;

public class LocationEntity : Entity<DefaultIdType>
{
    public LocationEntity()
    {
        Parts = new HashSet<PartEntity>();
        Assets = new HashSet<AssetEntity>();
        Storages = new HashSet<StorageEntity>();
        Children = new HashSet<LocationEntity>();
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DefaultIdType? ParentId { get; set; }
    public virtual LocationEntity? Parent { get; set; }
    public virtual ICollection<PartEntity> Parts { get; init; }
    public virtual ICollection<AssetEntity> Assets { get; init; }
    public virtual ICollection<StorageEntity> Storages { get; init; }
    public virtual ICollection<LocationEntity> Children { get; init; }
}
