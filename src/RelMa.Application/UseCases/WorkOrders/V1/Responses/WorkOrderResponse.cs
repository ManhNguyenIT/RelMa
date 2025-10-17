using RelMa.Application.UseCases.Checklists.V1.Responses;
using RelMa.Application.UseCases.Maintenances.V1.Responses;
using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Domain.WorkOrders;

namespace RelMa.Application.UseCases.WorkOrders.V1.Responses;

public class WorkOrderResponse
{
    public WorkOrderResponse()
    {
        Parts = [];
        Tasks = [];
        Checklists = [];
    }
    public required DefaultIdType Id { get; set; }
    public string? No { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Category Category { get; set; }
    public TimeSpan? Estimate { get; set; }
    public DefaultIdType? AssigneeId { get; set; }
    public DefaultIdType? RequestId { get; set; }
    public DefaultIdType? MaintenanceId { get; set; }
    public string[]? Images { get; set; }
    public virtual UserResponse? Assignee { get; set; }
    public virtual RequestResponse? Request { get; set; }
    public virtual MaintenanceResponse? Maintenance { get; set; }
    public virtual IEnumerable<PartResponse> Parts { get; set; }
    public virtual IEnumerable<TaskResponse> Tasks { get; set; }
    public virtual IEnumerable<ChecklistResponse> Checklists { get; set; }
}
