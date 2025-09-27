using RelMa.Domain.Parts;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Materials;

public class MaterialEntity : Entity<DefaultIdType>
{
    public MaterialEntity()
    {
        Parts = new HashSet<PartEntity>();
    }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public string[]? Images { get; set; }
    public Status Status { get; set; }
    public int Available => Parts.Sum(s => s.Quantity);
    public int Allocated { get; set; }
    public int OnHand { get; set; }
    public int Incoming { get; set; }
    public int Minimum { get; set; }
    public virtual ICollection<PartEntity> Parts { get; private set; }

    public void SetParts(ICollection<PartEntity> parts)
    {
        Parts = parts;
    }
}
