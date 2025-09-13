using RelMa.Domain.Checklists;
using RelMa.Domain.Maintenances;
using RelMa.Domain.Parts;
using RelMa.Domain.Requests;
using RelMa.Domain.Tasks;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.WorkOrders;

public class WorkOrderEntity : Entity<DefaultIdType>
{
    public WorkOrderEntity()
    {
        Parts = new HashSet<PartEntity>();
        Tasks = new HashSet<TaskEntity>();
        Checklists = new HashSet<ChecklistEntity>();
    }
    public required Status Status { get; set; }
    public DefaultIdType? RequestId { get; set; }
    public DefaultIdType? MaintenanceId { get; set; }
    public virtual RequestEntity? Request { get; set; }
    public virtual MaintenanceEntity? Maintenance { get; set; }
    public virtual ICollection<PartEntity> Parts { get; private set; }
    public virtual ICollection<TaskEntity> Tasks { get; private set; }
    public virtual ICollection<ChecklistEntity> Checklists { get; private set; }
}
