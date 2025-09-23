using RelMa.Domain.Assets;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.AssetLogs;

public class AssetLogEntity : Entity<DefaultIdType>
{
    public required DefaultIdType AssetId { get; set; }
    public required Status Status { get; set; }
    public required TimeSpan Duration { get; set; }
    public required DateTimeOffset Started { get; set; }
    public string? Description { get; set; }
    public virtual AssetEntity? Asset { get; set; }
}
