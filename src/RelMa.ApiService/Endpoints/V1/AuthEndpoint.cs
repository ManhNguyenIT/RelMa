using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using RelMa.ApiService.Abstractions;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class AuthEndpoint : IEndpoint
{
    private const string BaseUrl = "/auth";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Auth")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapGet("login", Login)
            .AllowAnonymous();

        route.MapGet("logout", Logout)
            .RequireAuthorization();
    }


    public static IResult Login(string? returnUrl = "/")
        => Results.Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, [OpenIdConnectDefaults.AuthenticationScheme]);

    public static async Task<IResult> Logout(IHttpContextAccessor accessor, string? returnUrl = "/")
    {
        var context = accessor.HttpContext;
        if (context is not null)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = returnUrl ?? "/" });
        }

        return Results.Redirect(returnUrl ?? "/");
    }
}
