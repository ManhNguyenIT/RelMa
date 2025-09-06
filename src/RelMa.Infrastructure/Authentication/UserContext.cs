using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Infrastructure.Caching;
using RelMa.Shared;
using RelMa.Shared.Exceptions;

namespace RelMa.Infrastructure.Authentication;
internal sealed class UserContext(
    IDistributedCache cache,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string? Name => httpContextAccessor.HttpContext?.User.GetName();
    public string? Company => httpContextAccessor.HttpContext?.User.GetCompany();
    public string? PhoneNumber => httpContextAccessor.HttpContext?.User.GetPhoneNumber();
    public string UserId => httpContextAccessor.HttpContext?.User.GetUserId()
        ?? throw new UnauthorizedException("UserId not found.");
    public string? TenantId => httpContextAccessor.HttpContext?.User.GetTenantId();
    public string? Username => httpContextAccessor.HttpContext?.User.GetUsername();

    public async Task<string> GetConnectionString() =>
        await cache.GetOrCreateAsync(
            key: $"default-connection:{TenantId}",
            factory: _ => Task.FromResult(configuration.GetConnectionString("DefaultConnection")),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: default)
        ?? throw new InvalidOperationException("Connection string not found.");

    public async Task<FileConfig> GetFileConfig() =>
        await cache.GetOrCreateAsync(
            key: $"file-config:{TenantId}",
            factory: _ => Task.FromResult(configuration.GetRequiredSection(nameof(FileConfig)).Get<FileConfig>()),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: default)
        ?? throw new InvalidOperationException("File config not found.");
}
