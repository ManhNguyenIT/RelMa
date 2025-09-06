using RelMa.Domain.Assets;
using RelMa.Shared.Abstractions.Entity;
using System.Text.Json;

namespace RelMa.Domain.Tasks;

public class TaskEntity : Entity<Ulid>
{
    public required Ulid AssetId { get; set; }
    public required TaskType Type { get; set; }
    public required JsonDocument Value { get; set; }
    public virtual AssetEntity? Asset { get; set; }
}
