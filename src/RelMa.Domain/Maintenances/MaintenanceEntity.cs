using RelMa.Domain.Assets;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Maintenances;

public class MaintenanceEntity : Entity<Ulid>
{
    public required Ulid AssetId { get; set; }
    public virtual AssetEntity? Asset { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
}
