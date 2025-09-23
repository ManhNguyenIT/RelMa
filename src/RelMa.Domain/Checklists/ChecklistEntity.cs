using RelMa.Domain.Tasks;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Checklists;

public class ChecklistEntity : Entity<DefaultIdType>
{
    public ChecklistEntity()
    {
        Tasks = new HashSet<TaskEntity>();
    }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DefaultIdType WorkOrderId { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
    public virtual ICollection<TaskEntity>? Tasks { get; private set; }

    public void SetTasks(ICollection<TaskEntity>? tasks)
    {
        Tasks = tasks;
    }
}
