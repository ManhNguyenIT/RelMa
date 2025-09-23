using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;

namespace RelMa.Application.UseCases.Checklists.V1.Responses;

public class ChecklistResponse
{
    public required DefaultIdType Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DefaultIdType? WorkOrderId { get; set; }
    public virtual WorkOrderResponse? WorkOrder { get; set; }
    public virtual IEnumerable<TaskResponse>? Tasks { get; set; }
}