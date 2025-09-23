using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;

namespace RelMa.Application.UseCases.Maintenances.V1.Responses;

public class MaintenanceResponse
{
    public required DefaultIdType Id { get; set; }
    public required DefaultIdType WorkOrderId { get; set; }
    public required string CronExpression { get; set; }
    public string[]? Images { get; set; }
    public virtual WorkOrderResponse? WorkOrder { get; set; }
    public virtual IEnumerable<AssetResponse>? Assets { get; set; }
}
