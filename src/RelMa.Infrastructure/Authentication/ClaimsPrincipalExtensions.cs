using System.Security.Claims;

namespace RelMa.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string[] GetTenantIds(this ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
            return [];

        return [.. principal.FindAll("tenant_id").Select(c => c.Value).Where(v => !string.IsNullOrEmpty(v))];
    }

    public static string? GetUsername(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirstValue("preferred_username");
    }

    public static string? GetName(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirstValue("name");
    }

    public static string? GetCompany(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("company")?.Value;
    }

    public static string? GetPhoneNumber(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("phone_number")?.Value;
    }
}
