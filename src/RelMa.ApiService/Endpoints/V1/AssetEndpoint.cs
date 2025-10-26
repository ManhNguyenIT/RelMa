using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using MimeKit;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Jobs;
using RelMa.Application.Abstractions.Services;
using RelMa.Application.UseCases.Assets.V1.Commands;
using RelMa.Application.UseCases.Assets.V1.Queries;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Infrastructure.Jobs;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class AssetEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/assets";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Assets")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapGet(string.Empty, List)
            .RequireAuthorization();

        route.MapGet("{id}", Info)
            .RequireAuthorization()
            .Produces(StatusCodes.Status404NotFound);

        route.MapGet("{id}/status", Status)
            .RequireAuthorization();

        route.MapPost(string.Empty, Create)
            .RequireAuthorization();

        route.MapPut(string.Empty, Update)
            .RequireAuthorization();

        route.MapDelete(string.Empty, Delete)
            .RequireAuthorization();

        route.MapPost("upload", Upload)
            .RequireAuthorization()
            .DisableAntiforgery();

        route.MapPost("import", Import)
            .RequireAuthorization()
            .DisableAntiforgery();

        route.MapGet("export", Export)
            .RequireAuthorization();

        route.MapGet("template", Template)
            .RequireAuthorization();
    }

    public static async Task<PagedResult<AssetResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetAssetQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:assets",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetAssetQuery, PagedResult<AssetResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<AssetResponse?> Info(
        DefaultIdType id,
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:asset-info:{id}",
            param: id,
            factory: async token => await mediator.SendQueryAsync<GetAssetInfoQuery, AssetResponse>(new GetAssetInfoQuery(id), token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<IResult> Status(
        DefaultIdType id,
        IDistributedCache cache,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"status",
            param: id,
            factory: _ => Task.FromResult(new Dictionary<string, object>() { { "status", ProcessingStatus.None } }),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );
        return Results.Ok(result);
    }

    public static async Task<DefaultIdType> Create(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] CreateAssetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateAssetCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:assets",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

    public static async Task<DefaultIdType> Update(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] UpdateAssetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateAssetCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:assets",
            $"{userContext.TenantId}:asset-info:{command.Id}",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

    public static async Task<bool> Delete(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [AsParameters] DeleteAssetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteAssetCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:assets",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

    public static async Task<IResult> Upload(
        IFormFile file,
        IDistributedCache cache,
        IJobScheduler scheduler,
        IImageService imageService,
        LinkGenerator linkGenerator,
        IHttpContextAccessor accessor,
        CancellationToken cancellationToken)
    {
        if (accessor.HttpContext is null)
            return Results.BadRequest("No HttpContext.");

        var (id, fileName) = await imageService.SaveImagesAsync(file, cancellationToken);

        var status = new Dictionary<string, object>
        {
            ["status"] = ProcessingStatus.Queued,
            ["message"] = string.Empty
        };

        await cache.SetAsync(
            key: "status",
            param: id,
            factory: _ => Task.FromResult(status),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
            cancellationToken: default
        );

        var result = new Dictionary<string, object>
        {
            { "id", id },
            { "fileName", fileName }
        };

        await scheduler.TriggerJob(nameof(GenerateThumbnailsJob), result, cancellationToken);

        return Results.Accepted(
            linkGenerator.GetPathByAction(
               httpContext: accessor.HttpContext,
               action: "status",
               controller: "assets",
               values: new { id }
            ),
            result
        );
    }

    public static async Task<IResult> Import(
        IFormFile file,
        IMediator mediator,
        IJobScheduler scheduler,
        IDistributedCache cache,
        IUserContext userContext,
        LinkGenerator linkGenerator,
        IHttpContextAccessor accessor,
        CancellationToken cancellationToken)
    {
        if (accessor.HttpContext is null)
            return Results.BadRequest("No HttpContext.");

        var (id, fileName) = await mediator.SendCommandAsync<ImportAssetCommand, (DefaultIdType, string)>(new ImportAssetCommand(file), cancellationToken);

        var result = new Dictionary<string, object>
        {
            { "id", id },
            { "fileName", fileName },
            { "tenantId", userContext.TenantId ?? string.Empty }
        };

        var status = new Dictionary<string, object>
        {
            ["status"] = ProcessingStatus.Queued,
            ["processed"] = 0,
            ["message"] = string.Empty,
        };

        await cache.SetAsync(
            key: "status",
            param: id,
            factory: _ => Task.FromResult(status),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
            cancellationToken: default
        );

        await scheduler.TriggerJob(nameof(ImportAssetsJob), result, cancellationToken);

        return Results.Accepted(
            linkGenerator.GetPathByAction(
               httpContext: accessor.HttpContext,
               action: "status",
               controller: "assets",
               values: new { id }
            ),
            id
        );
    }

    public static async Task<IResult> Export(
        IMediator mediator,
        [AsParameters] ExportAssetQuery query)
    {
        var fileName = $"assets_exported_{DateTime.Now:yyyyMMdd}.xlsx";
        var mimeType = MimeTypes.GetMimeType(fileName);
        var stream = await mediator.SendQueryAsync<ExportAssetQuery, MemoryStream>(query);
        stream.Position = 0;
        return Results.File(stream, mimeType, Path.GetFileNameWithoutExtension(fileName), enableRangeProcessing: true);
    }

    public static IResult Template(IWebHostEnvironment environment)
    {
        string fileName = Path.Combine(environment.ContentRootPath, "assets", "templates", "asset.xlsx");
        var mimeType = MimeTypes.GetMimeType(fileName);
        return Results.File(fileName, mimeType, Path.GetFileNameWithoutExtension(fileName), enableRangeProcessing: true);
    }
}
