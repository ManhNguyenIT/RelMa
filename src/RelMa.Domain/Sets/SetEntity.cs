using RelMa.Domain.Parts;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Sets;

public class SetEntity : Entity<DefaultIdType>
{
    public SetEntity()
    {
        Parts = new HashSet<PartEntity>();
    }
    public required string Name { get; set; }
    public virtual ICollection<PartEntity> Parts { get; private set; }

    public void AddParts(IEnumerable<PartEntity> parts)
    {
        Parts = [.. parts];
    }
}
