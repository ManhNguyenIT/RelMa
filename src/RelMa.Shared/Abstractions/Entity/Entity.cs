using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelMa.Shared.Abstractions.Entity;
public abstract class Entity<TKey> : AggregateRoot, IEntity<TKey>, IAuditable<TKey>, ITenantTracking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required TKey Id { get; set; }
    public string? TenantId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public TKey? CreatedBy { get; set; }
    public TKey? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public TKey? DeletedBy { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public void Undo()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = default;
    }
}
