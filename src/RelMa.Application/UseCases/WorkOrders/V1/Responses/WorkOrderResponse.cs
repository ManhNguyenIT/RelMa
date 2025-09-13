using RelMa.Application.UseCases.Checklists.V1.Responses;
using RelMa.Application.UseCases.Maintenances.V1.Responses;
using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Domain.Maintenances;
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
    public required Status Status { get; set; }
    public DefaultIdType? RequestId { get; set; }
    public virtual RequestResponse? Request { get; set; }
    public DefaultIdType? MaintenanceId { get; set; }
    public virtual MaintenanceResponse? Maintenance { get; set; }
    public virtual IEnumerable<PartResponse> Parts { get; set; }
    public virtual IEnumerable<TaskResponse> Tasks { get; set; }
    public virtual IEnumerable<ChecklistResponse> Checklists { get; set; }
}
