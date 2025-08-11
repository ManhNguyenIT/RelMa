namespace RelMa.Infrastructure.Storage;

public sealed class FileConfig
{
    public required string Endpoint { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required bool UseHttps { get; set; }
    public required string TempBucket { get; set; }
    public required string TargetBucket { get; set; }
}