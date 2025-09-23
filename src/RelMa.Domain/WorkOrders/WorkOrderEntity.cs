using RelMa.Domain.Checklists;
using RelMa.Domain.Files;
using RelMa.Domain.Maintenances;
using RelMa.Domain.Parts;
using RelMa.Domain.Requests;
using RelMa.Domain.Tasks;
using RelMa.Domain.Users;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.WorkOrders;

public class WorkOrderEntity : Entity<DefaultIdType>
{
    public WorkOrderEntity()
    {
        Files = new HashSet<FileEntity>();
        Parts = new HashSet<PartEntity>();
        Tasks = new HashSet<TaskEntity>();
        Checklists = new HashSet<ChecklistEntity>();
    }

    public string? No { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Category Category { get; set; }
    public TimeSpan? Estimate { get; set; }
    public DefaultIdType? RequestId { get; set; }
    public DefaultIdType? MaintenanceId { get; set; }
    public string[]? Images { get; set; }
    public string? AssigneeId { get; set; }
    public virtual UserEntity? Assignee { get; set; }
    public virtual RequestEntity? Request { get; set; }
    public virtual MaintenanceEntity? Maintenance { get; set; }
    public virtual ICollection<PartEntity> Parts { get; init; }
    public virtual ICollection<TaskEntity> Tasks { get; init; }
    public virtual ICollection<FileEntity>? Files { get; init; }
    public virtual ICollection<ChecklistEntity> Checklists { get; init; }
}
