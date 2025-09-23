using RelMa.Domain.AssetLogs;
using RelMa.Domain.Files;
using RelMa.Domain.Locations;
using RelMa.Domain.Maintenances;
using RelMa.Domain.Parts;
using RelMa.Domain.Requests;
using RelMa.Domain.Tasks;
using RelMa.Domain.Teams;
using RelMa.Shared.Abstractions.Entity;
using System.Text.Json;

namespace RelMa.Domain.Assets;

public class AssetEntity : Entity<DefaultIdType>
{
    public AssetEntity()
    {
        Files = new HashSet<FileEntity>();
        Parts = new HashSet<PartEntity>();
        Tasks = new HashSet<TaskEntity>();
        Teams = new HashSet<TeamEntity>();
        Requests = new HashSet<RequestEntity>();
        AssetLogs = new HashSet<AssetLogEntity>();
        Maintenances = new HashSet<MaintenanceEntity>();
    }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required Status Status { get; set; }
    public required DefaultIdType LocationId { get; set; }
    public string? Area { get; set; }
    public string? SerialNumber { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Model { get; set; }
    public string[]? Images { get; set; }
    public JsonDocument? Metadata { get; set; }
    public virtual LocationEntity? Location { get; set; }
    public virtual ICollection<FileEntity> Files { get; init; }
    public virtual ICollection<PartEntity> Parts { get; init; }
    public virtual ICollection<TaskEntity> Tasks { get; init; }
    public virtual ICollection<TeamEntity> Teams { get; init; }
    public virtual ICollection<RequestEntity> Requests { get; init; }
    public virtual ICollection<AssetLogEntity> AssetLogs { get; init; }
    public virtual ICollection<MaintenanceEntity> Maintenances { get; init; }
}
