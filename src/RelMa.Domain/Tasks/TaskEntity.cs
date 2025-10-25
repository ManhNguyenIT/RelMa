using RelMa.Domain.Assets;
using RelMa.Domain.Checklists;
using RelMa.Shared.Abstractions.Entity;
using System.Text.Json;

namespace RelMa.Domain.Tasks;

public class TaskEntity : Entity<DefaultIdType>
{
    public DefaultIdType? AssetId { get; set; }
    public DefaultIdType? ChecklistId { get; set; }
    public required TaskType Type { get; set; }
    public required JsonDocument Value { get; set; }
    public virtual AssetEntity? Asset { get; set; }
    public virtual ChecklistEntity? Checklist { get; set; }
}
