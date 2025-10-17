using RelMa.Shared;

namespace RelMa.Application.Abstractions.Authentication;

public interface IUserContext
{
    DefaultIdType UserId { get; }
    string? Username { get; }
    string? Name { get; }
    string? Company { get; }
    string? TenantId { get; }
    string? PhoneNumber { get; }
    Task<string> GetConnectionString();
    Task<FileConfig> GetFileConfig();
}

