using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Responses;

public class RequestResponse
{
    public required DefaultIdType Id { get; set; }
    public required DefaultIdType AssetId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required Priority Priority { get; set; }
    public string? Image { get; set; }
    public required Status Status { get; set; }

    public virtual AssetResponse? Asset { get; set; }
}
