using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Application.UseCases.Files.V1.Responses;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Responses;

public class RequestResponse
{
    public required DefaultIdType Id { get; set; }
    public required DefaultIdType AssetId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Category Category { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public string[]? Images { get; set; }
    public DefaultIdType? WorkOrderId { get; set; }
    public virtual AssetResponse? Asset { get; set; }
    public virtual WorkOrderResponse? WorkOrder { get; set; }
    public virtual IEnumerable<FileResponse>? Files { get; set; }
}
