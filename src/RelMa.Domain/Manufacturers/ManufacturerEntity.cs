using RelMa.Domain.Assets;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Manufacturers;

public class ManufacturerEntity : Entity<DefaultIdType>
{
    public ManufacturerEntity()
    {
        Assets = new HashSet<AssetEntity>();
    }
    public required string Name { get; set; }
    public virtual ICollection<AssetEntity> Assets { get; }
}
