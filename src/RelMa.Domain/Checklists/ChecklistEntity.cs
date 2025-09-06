using RelMa.Domain.Tasks;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Checklists;

public class ChecklistEntity : Entity<Ulid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Ulid TaskId { get; set; }
    public virtual TaskEntity? Task { get; set; }
    public required Ulid WorkOrderId { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
}
