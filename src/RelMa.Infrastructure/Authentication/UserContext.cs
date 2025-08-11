using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Infrastructure.Caching;
using RelMa.Shared.Exceptions;

namespace RelMa.Infrastructure.Authentication;
internal sealed class UserContext(
    IDistributedCache cache,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string UserId => httpContextAccessor.HttpContext?.User.GetUserId()
        ?? throw new UnauthorizedException("UserId not found.");
    public string TenantId => httpContextAccessor.HttpContext?.User.GetTenantId()
        ?? throw new UnauthorizedException("TenantId not found.");

    public async Task<string> GetConnectionString() =>
        await cache.GetOrCreateAsync(
            key: $"default-connection:{TenantId}",
            factory: _ => Task.FromResult(configuration.GetConnectionString("DefaultConnection")),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: default)
        ?? throw new InvalidOperationException("Connection string not found.");
}
