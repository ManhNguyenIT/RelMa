using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Files;

public class FileEntity:Entity<Ulid>
{
    public required string Name { get; set; }
    public required string Ext { get; set; }
    public required string Source { get; set; }
    public long Size { get; set; }
}
