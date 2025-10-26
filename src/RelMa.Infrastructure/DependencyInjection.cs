using Asp.Versioning;
using Cortex.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using Quartz;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Abstractions.Jobs;
using RelMa.Application.Abstractions.Services;
using RelMa.Authorization;
using RelMa.Infrastructure.Authentication;
using RelMa.Infrastructure.Database;
using RelMa.Infrastructure.Database.Interceptors;
using RelMa.Infrastructure.Jobs;
using RelMa.Infrastructure.Services;
using RelMa.Shared;
using RelMa.Shared.Events;
using StackExchange.Redis;
using System.Reflection;

namespace RelMa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCache(configuration);
        services.AddOpenApi(configuration);

        services.AddQuartz(configuration);
        services.AddDatabase(configuration);
        services.AddFileStorage(configuration);

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IImageService, ImageService>();

        services.AddAuthentication(configuration);
        services.AddAuthorization(configuration);

        return services;
    }

    public static IServiceCollection AddQuartz(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz(options =>
        {
            options.UsePersistentStore(persistentOptions =>
            {
                persistentOptions.UsePostgres(config =>
                {
                    config.ConnectionString = configuration.GetConnectionString("SchedulerConnection")
                        ?? throw new InvalidOperationException($"Failed to load SchedulerConnection from environment.");
                    config.TablePrefix = "scheduler.qrtz_";
                });

                persistentOptions.UseClustering();
                persistentOptions.UseProperties = true;
                persistentOptions.UseNewtonsoftJsonSerializer();
            });

            var jobs = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IJob).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var job in jobs)
            {
                options.AddJob(job, new JobKey(job.Name), opts => opts
                    .WithIdentity(job.Name)
                    .StoreDurably());
            }
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.AddScoped<IJobScheduler, QuartzJobScheduler>();

        return services;
    }

    public static IServiceCollection AddFileStorage(
        this IServiceCollection services,
        IConfiguration configuration)
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

    internal static IServiceCollection AddCache(
        this IServiceCollection services,
        IConfiguration configuration)
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

        var multiplexer = ConnectionMultiplexer.Connect(redisConfig.Configuration);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        services.AddDataProtection()
            .SetApplicationName(redisConfig.InstanceName)
            .PersistKeysToStackExchangeRedis(multiplexer, $"{redisConfig.InstanceName}data-protection-keys");

        return services;
    }

    public static IServiceCollection AddOpenApi(
        this IServiceCollection services,
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

            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

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

            options.AddSecurityDefinition("oidc", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Cookie,
                Type = SecuritySchemeType.ApiKey,
                Name = CookieAuthenticationDefaults.AuthenticationScheme,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        },
                        In = ParameterLocation.Header,
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                    },
                    authConfig.Scopes
                },
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
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "oidc",
                            Type = ReferenceType.SecurityScheme,
                        },
                        In = ParameterLocation.Cookie,
                        Name = CookieAuthenticationDefaults.AuthenticationScheme,
                        Scheme = CookieAuthenticationDefaults.AuthenticationScheme,
                    },
                    authConfig.Scopes
                }
            });

            options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName is null || docName == apiDesc.GroupName);
        });

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, TenantEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException($"Failed to load DefaultConnection from environment.");

        return services.AddDbContext<ApplicationDbContext>((provider, options) => options
            .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
            .AddInterceptors(provider.GetServices<ISaveChangesInterceptor>())
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information));
    }

    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authConfig = configuration.GetRequiredSection(nameof(AuthConfig)).Get<AuthConfig>()
            ?? throw new InvalidOperationException($"Failed to load {nameof(AuthConfig)} from configuration.");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "DefaultScheme";
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddPolicyScheme("DefaultScheme", null, options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
                if (authHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                }

                return CookieAuthenticationDefaults.AuthenticationScheme;
            };
        })
        .AddCookie(options =>
        {
            options.SlidingExpiration = true;
            options.LoginPath = "/auth/login";
            options.LogoutPath = "/auth/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        })
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
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogWarning("⚠️ JWT challenge triggered: {Error} - {ErrorDescription}", context.Error, context.ErrorDescription);
                    return Task.CompletedTask;
                },
                OnForbidden = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogWarning("⛔ JWT access forbidden for user {Sub}", context.HttpContext.User?.FindFirst("sub")?.Value);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogInformation("✅ JWT validated successfully for user {Sub}", context.Principal?.FindFirst("sub")?.Value);
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogInformation("🔑 JWT message received: {Message}", context.Token ?? "No token found");
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogError(context.Exception, "❌ JWT validation failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                },
            };
        })
        .AddOpenIdConnect(options =>
        {
            options.SaveTokens = true;
            options.ResponseType = "code";
            options.ResponseMode = "query";
            options.RequireHttpsMetadata = false;
            options.ClientId = authConfig.ClientId;
            options.Authority = authConfig.Authority;
            options.CallbackPath = "/auth/oidc-callback";
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClientSecret = authConfig.ClientSecret;

            options.NonceCookie.SameSite = SameSiteMode.Lax;
            options.NonceCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            options.CorrelationCookie.SameSite = SameSiteMode.Lax;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            foreach (var scope in authConfig.Scopes)
            {
                options.Scope.Add(scope);
            }

            options.Events = new OpenIdConnectEvents
            {
                OnRedirectToIdentityProvider = context =>
                {
                    var request = context.Request;

                    if (request.Headers.TryGetValue("X-Forwarded-Proto", out var proto))
                        context.ProtocolMessage.RedirectUri = $"{proto}://{request.Host}{request.PathBase}{options.CallbackPath}";

                    return Task.CompletedTask;
                },
                OnRemoteFailure = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectEvents>>();
                    logger.LogError(context.Failure, "❌ OIDC remote failure: {Message}", context.Failure?.Message);
                    context.Response.Redirect("/");
                    context.HandleResponse();
                    return Task.CompletedTask;
                },
                OnUserInformationReceived = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectEvents>>();
                    logger.LogInformation("👤 OIDC user info received: {User}", context.User);
                    return Task.CompletedTask;
                },
                OnTokenResponseReceived = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectEvents>>();
                    logger.LogInformation("🔐 OIDC token response received: {TokenResponse}", context.TokenEndpointResponse);
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectEvents>>();
                    logger.LogInformation("🔑 OIDC message received: {Message}", context.ProtocolMessage);
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogInformation("✅ JWT validated successfully for user {Sub}", context.Principal?.FindFirst("sub")?.Value);

                    var accessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
                    if (accessor.HttpContext != null)
                    {
                        accessor.HttpContext.User = context.Principal ?? new System.Security.Claims.ClaimsPrincipal();
                    }
                    var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
                    await mediator.PublishAsync(new UserLoggedInEvent());
                },
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                    logger.LogError(context.Exception, "❌ JWT validation failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddAuthorization(
        this IServiceCollection services,
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
