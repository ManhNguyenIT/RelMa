using RelMa.Domain.Parts;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Materials;

public class MaterialEntity : Entity<DefaultIdType>
{
    public MaterialEntity()
    {
        Items = new HashSet<PartEntity>();
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int MinQty { get; set; }
    public int AvailableQty { get; set; }
    public int IncomingQty { get; set; }
    public int AllocatedQty { get; set; }
    public virtual ICollection<PartEntity> Items { get; }
}
