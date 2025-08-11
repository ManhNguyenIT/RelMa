using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Abstractions.Services;
using RelMa.Authorization;
using RelMa.Infrastructure.Authentication;
using RelMa.Infrastructure.Database;
using RelMa.Infrastructure.Database.Interceptors;
using RelMa.Infrastructure.Storage;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCache(configuration);
        services.AddOpenApi(configuration);
        services.AddDatabase(configuration);

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        services.AddAuthentication(configuration);
        services.AddAuthorization(configuration);

        return services;
    }

    public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<FileConfig>()
            .Bind(configuration.GetSection(nameof(FileConfig)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped(provider =>
        {
            var fileConfig = configuration.GetRequiredSection(nameof(FileConfig)).Get<FileConfig>()
                ?? throw new InvalidOperationException($"Failed to load {nameof(FileConfig)} from configuration.");

            var minioClient = new MinioClient()
                .WithEndpoint(fileConfig.Endpoint)
                .WithCredentials(fileConfig.AccessKey, fileConfig.SecretKey)
                .WithSSL(fileConfig.UseHttps)
                .Build();

            return minioClient;
        });

        services.AddScoped<IFileService, FileService>();

        return services;
    }

    internal static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RedisConfig>()
            .Bind(configuration.GetSection(nameof(RedisConfig)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var redisConfig = configuration.GetRequiredSection(nameof(RedisConfig)).Get<RedisConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(RedisConfig)} from configuration.");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig.Configuration;
            options.InstanceName = redisConfig.InstanceName;
        });
        services.AddSingleton<IConnectionMultiplexer>(
            _ => ConnectionMultiplexer.Connect(redisConfig.Configuration)
        );

        return services;
    }

    internal static IServiceCollection AddOpenApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        var appConfig = configuration.GetRequiredSection(nameof(AppConfig)).Get<AppConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AppConfig)} from configuration.");

        var authConfig = configuration.GetRequiredSection(nameof(AuthConfig)).Get<AuthConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AuthConfig)} from configuration.");

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(options =>
        {
            foreach (var version in appConfig.SupportedVersions)
            {
                options.SwaggerDoc(version, new OpenApiInfo() { Version = version });
            }

            options.CustomSchemaIds(type => type.ToString().Replace('+', '.'));
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Name = JwtBearerDefaults.AuthenticationScheme,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        Scopes = authConfig.Scopes.ToDictionary(scope => scope, scope => scope),
                        TokenUrl = new Uri($"{authConfig.Authority}/protocol/openid-connect/token", UriKind.Absolute),
                        AuthorizationUrl = new Uri($"{authConfig.Authority}/protocol/openid-connect/auth", UriKind.Absolute),
                    }
                },
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Id = "oauth2",
                            Type = ReferenceType.SecurityScheme,
                        },
                        In = ParameterLocation.Header,
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                    },
                    authConfig.Scopes
                }
            });

            options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName is null || docName == apiDesc.GroupName);
        });

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, TenantEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException($"Failed to load DefaultConnection from environment.");

        services.AddDbContext<ApplicationDbContext>((sp, options) => options
            .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
            .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
            .UseUpperSnakeCaseNamingConvention());

        return services;
    }

    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authConfig = configuration.GetRequiredSection(nameof(AuthConfig)).Get<AuthConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AuthConfig)} from configuration.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = authConfig.Authority;
#pragma warning disable CA5404 // Do not disable token validation checks
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false, //Issue: https://github.com/keycloak/keycloak/issues/31023
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
#pragma warning restore CA5404 // Do not disable token validation checks
            })
            .AddOpenIdConnect(options =>
            {
                options.SaveTokens = true;
                options.ResponseType = "code";
                options.RequireHttpsMetadata = false;
                options.ClientId = authConfig.ClientId;
                options.Authority = authConfig.Authority;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientSecret = authConfig.ClientSecret;
                foreach (var scope in authConfig.Scopes)
                {
                    options.Scope.Add(scope);
                }
            });

        return services;
    }

    public static IServiceCollection AddAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        var appConfig = configuration.GetRequiredSection(nameof(AppConfig)).Get<AppConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AppConfig)} from configuration.");

        var policyBuilder = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser();
        if (appConfig.RequireRoles.Length != 0)
        {
            policyBuilder.RequireRole(appConfig.RequireRoles);
        }
        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(policyBuilder.Build());

        services.AddScoped<PermissionProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.StartsWithSegments("/swagger", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                context.Response.Headers.Append("Pragma", "no-cache");
                context.Response.Headers.Append("Expires", "0");
            }
            await next(context);
        });

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            }
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

            options.EnablePersistAuthorization();
            options.OAuthUsePkce();
        });

        return app;
    }
}
