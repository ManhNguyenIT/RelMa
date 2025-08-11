using System.Security.Claims;

namespace RelMa.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string? GetTenantId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue("tenant_id");
    }
}
