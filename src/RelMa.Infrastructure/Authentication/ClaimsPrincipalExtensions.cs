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

    public static string? GetTenantId(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirstValue("tenant_id");
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
