namespace RelMa.Shared;

public class AppConfig
{
    public required string[] CorsOrigins { get; set; } = [];
    public required string[] RequireRoles { get; set; } = [];
    public required string[] SupportedVersions { get; set; } = [];
}
