using RelMa.Domain.Assets;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Maintenances;

public class MaintenanceEntity : Entity<DefaultIdType>
{
    public MaintenanceEntity()
    {
        Assets = new HashSet<AssetEntity>();
    }
    public required DefaultIdType WorkOrderId { get; set; }
    public required string CronExpression { get; set; }
    public string[]? Images { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
    public virtual ICollection<AssetEntity>? Assets { get; init; }
}
