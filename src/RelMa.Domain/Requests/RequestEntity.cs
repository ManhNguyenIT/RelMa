using RelMa.Domain.Assets;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Requests;

public class RequestEntity : Entity<DefaultIdType>
{
    public required DefaultIdType AssetId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required Priority Priority { get; set; }
    public string? Image { get; set; }
    public required Status Status { get; set; }

    public virtual AssetEntity? Asset { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
}
