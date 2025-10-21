using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Jobs;
using RelMa.Application.Abstractions.Services;
using RelMa.Application.UseCases.Materials.V1.Commands;
using RelMa.Application.UseCases.Materials.V1.Queries;
using RelMa.Application.UseCases.Materials.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class MaterialEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/materials";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Materials")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapGet(string.Empty, List)
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
        [AsParameters] GetMaterialQuery query,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:materials",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetMaterialQuery, PagedResult<MaterialResponse>>(query, token),
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
        [FromBody] CreateMaterialCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateMaterialCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:materials",
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
        [FromBody] UpdateMaterialCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateMaterialCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:materials",
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
        [FromBody] DeleteMaterialCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteMaterialCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:materials",
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

    public static async Task<IResult> Export([FromQuery] string fileName)
    {
        await Task.CompletedTask;
        using var stream = new MemoryStream();
        stream.Position = 0;
        return Results.File(stream, "application/octet-stream", fileName);
    }

    public static async Task<IResult> Template([FromQuery] string fileName)
    {
        await Task.CompletedTask;
        using var stream = new MemoryStream();
        stream.Position = 0;
        return Results.File(stream, "application/octet-stream", fileName);
    }
}