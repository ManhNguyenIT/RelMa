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
    IHttpContextAccessor contextAccessor) : IUserContext
{
    public string? Name => contextAccessor.HttpContext?.User.GetName();
    public string? Company => contextAccessor.HttpContext?.User.GetCompany();
    public string? PhoneNumber => contextAccessor.HttpContext?.User.GetPhoneNumber();
    public string UserId => contextAccessor.HttpContext?.User.GetUserId()
        ?? throw new UnauthorizedException("UserId not found.");
    public string? TenantId
    {
        get
        {
            var httpContext = contextAccessor.HttpContext;
            if (httpContext == null) return null;

            var tenantId = httpContext.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            var tenantIds = httpContext.User.GetTenantIds();

            return string.IsNullOrEmpty(tenantId)
                ? tenantIds.FirstOrDefault()
                : tenantIds.Contains(tenantId) ? tenantId : null;
        }
    }
    public string? Username => contextAccessor.HttpContext?.User.GetUsername();

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
