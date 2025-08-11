namespace RelMa.Shared;

public class AuthConfig
{
    public string[] Scopes { get; set; } = [];
    public required string ClientId { get; set; }
    public required string Authority { get; set; }
    public required string ClientSecret { get; set; }
}

