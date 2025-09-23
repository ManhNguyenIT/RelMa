using RelMa.Domain.Assets;
using RelMa.Domain.Files;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Requests;

public class RequestEntity : Entity<DefaultIdType>
{
    public required DefaultIdType AssetId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public Category Category { get; set; }
    public Priority Priority { get; set; }
    public string[]? Images { get; set; }
    public DefaultIdType? WorkOrderId { get; set; }
    public virtual AssetEntity? Asset { get; set; }
    public virtual WorkOrderEntity? WorkOrder { get; set; }
    public virtual ICollection<FileEntity>? Files { get; init; }
}
