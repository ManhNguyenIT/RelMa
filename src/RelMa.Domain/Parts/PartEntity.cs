using RelMa.Domain.Items;
using RelMa.Domain.Sets;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Parts;

public class PartEntity : Entity<Ulid>
{
    public PartEntity()
    {
        Sets = new HashSet<SetEntity>();
        Items = new HashSet<ItemEntity>();
    }
    public required string Name { get; set; }
    public required string PartNumber { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public virtual ICollection<SetEntity> Sets { get; }
    public virtual ICollection<ItemEntity> Items { get; private set; }

    public void AddItems(IEnumerable<ItemEntity> items)
    {
        Items = [.. items];
    }
}
