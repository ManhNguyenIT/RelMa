using RelMa.Domain.Assets;
using RelMa.Domain.Requests;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Files;

public class FileEntity : Entity<DefaultIdType>
{
    public required string Name { get; set; }
    public required string Ext { get; set; }
    public required string Source { get; set; }
    public long Size { get; set; }
    public DefaultIdType? AssetId { get; set; }
    public virtual AssetEntity? Asset { get; set; }
    public DefaultIdType? RequestId { get; set; }
    public virtual RequestEntity? Request { get; set; }
    public DefaultIdType? WorkOrderId { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
}
