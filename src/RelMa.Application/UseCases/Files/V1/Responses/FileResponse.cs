namespace RelMa.Application.UseCases.Files.V1.Responses;

public class FileResponse
{
    public required DefaultIdType Id { get; set; }
    public required string Name { get; set; }
    public required string Ext { get; set; }
    public required string Source { get; set; }
    public long Size { get; set; }
}
