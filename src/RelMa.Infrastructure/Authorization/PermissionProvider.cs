using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace RelMa.Authorization;
internal sealed class PermissionProvider(IHttpContextAccessor httpContextAccessor)
{
    public Task<HashSet<string>> GetUserPermissions()
    {
        var permissions = new HashSet<string>();
        var user = httpContextAccessor.HttpContext?.User;
        var aud = httpContextAccessor.HttpContext?.User.FindFirstValue("aud");

        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            return Task.FromResult(permissions);
        }

        var realmAccess = user.FindFirst("realm_access");
        if (realmAccess != null)
        {
            using var doc = JsonDocument.Parse(realmAccess.Value);
            if (doc.RootElement.TryGetProperty("roles", out var roles))
            {
                foreach (var role in roles.EnumerateArray())
                {
                    var value = role.GetString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        permissions.Add(value);
                    }
                }
            }
        }

        var resourceAccess = user.FindFirst("resource_access");
        if (aud is not null && resourceAccess is not null)
        {
            using var doc = JsonDocument.Parse(resourceAccess.Value);
            if (doc.RootElement.TryGetProperty(aud, out var client)
                && client.TryGetProperty("roles", out var roles))
            {
                foreach (var role in roles.EnumerateArray())
                {
                    var value = role.GetString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        permissions.Add(value);
                    }
                }
            }
        }

        return Task.FromResult(permissions);
    }
}
