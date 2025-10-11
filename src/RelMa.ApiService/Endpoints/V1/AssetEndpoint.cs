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
            .RequireAuthorization();

        route.MapGet("export", Export)
            .RequireAuthorization();

        route.MapGet("template", Template)
            .RequireAuthorization();
    }

    public static async Task<IResult> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetAssetQuery query,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:assets",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetAssetQuery, PagedResult<AssetResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

        return Results.Ok(result);
    }

    public static async Task<IResult> Info(
        DefaultIdType id,
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:asset-info:{id}",
            param: id,
            factory: async token => await mediator.SendQueryAsync<GetAssetInfoQuery, AssetResponse>(new GetAssetInfoQuery(id), token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

        return Results.Ok(result);
    }

    public static async Task<IResult> Create(
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
        return Results.Ok(result);
    }

    public static async Task<IResult> Update(
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
        return Results.Ok(result);
    }

    public static async Task<IResult> Delete(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] DeleteAssetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteAssetCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:assets",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return Results.Ok(result);
    }


    public static async Task<IResult> Upload(
        IFormFile file,
        IJobScheduler scheduler,
        IImageService imageService,
        CancellationToken cancellationToken)
    {
        var fileName = await imageService.SaveImagesAsync(file, cancellationToken);

        await scheduler.TriggerJob(scheduler.ProcessThumbnailJob, new Dictionary<string, object>
        {
            { "fileName", fileName }
        });

        return Results.Ok(fileName);
    }


    public static async Task<IResult> Import()
    {
        await Task.CompletedTask;
        return Results.Ok();
    }

    public static async Task<IResult> Export(
        IMediator mediator,
        [AsParameters] ExportAssetQuery query)
    {
        var fileName = $"asset_exported_{DateTime.Now:yyyyMMdd}.xlsx";
        var mimeType = MimeTypes.GetMimeType(fileName);
        var stream = await mediator.SendQueryAsync<ExportAssetQuery, MemoryStream>(query);
        return Results.File(stream, mimeType, Path.GetFileNameWithoutExtension(fileName), enableRangeProcessing: true);
    }

    public static IResult Template(IWebHostEnvironment environment)
    {
        string fileName = Path.Combine(environment.ContentRootPath, "assets", "templates", "asset.xlsx");
        var mimeType = MimeTypes.GetMimeType(fileName);
        return Results.File(fileName, mimeType, Path.GetFileNameWithoutExtension(fileName), enableRangeProcessing: true);
    }
}
