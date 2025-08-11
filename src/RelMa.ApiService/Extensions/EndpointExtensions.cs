using Microsoft.Extensions.DependencyInjection.Extensions;
using RelMa.ApiService.Abstractions;
using System.Reflection;

namespace RelMa.ApiService.Extensions;

internal static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = [.. assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        var builder = (IEndpointRouteBuilder?)routeGroupBuilder ?? app;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission) => app.RequireAuthorization(permission);
}
