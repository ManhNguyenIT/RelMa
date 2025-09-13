using RelMa.ApiService.Extensions;
using RelMa.Shared;

namespace RelMa.ApiService.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddPresentation(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptionsAndValidation(configuration);

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddEndpointsApiExplorer();

        return services;
    }

    internal static IServiceCollection AddOptionsAndValidation(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<AppConfig>()
            .Bind(configuration.GetSection(nameof(AppConfig)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<AuthConfig>()
            .Bind(configuration.GetSection(nameof(AuthConfig)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        return services;
    }

    internal static IServiceCollection AddCors(this IServiceCollection services,
        IConfiguration configuration)
    {
        var appConfig = configuration.GetRequiredSection(nameof(AppConfig)).Get<AppConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AppConfig)} from configuration.");

        services.AddCors(options => options.AddDefaultPolicy(
                policy =>
                {
                    policy = appConfig.CorsOrigins.Length == 0
                        ? policy.AllowAnyOrigin()
                        : policy.WithOrigins(appConfig.CorsOrigins).AllowCredentials();

                    policy.AllowAnyHeader().AllowAnyMethod();
                }));

        return services;
    }
}
