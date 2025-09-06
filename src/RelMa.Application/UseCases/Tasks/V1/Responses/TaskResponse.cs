using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Tasks;
using System.Text.Json;

namespace RelMa.Application.UseCases.Tasks.V1.Responses;

public class TaskResponse
{
    public required Ulid Id { get; set; }
    public required Ulid AssetId { get; set; }
    public required TaskType Type { get; set; }
    public required JsonDocument Value { get; set; }
    public virtual AssetResponse? Asset { get; set; }
}
