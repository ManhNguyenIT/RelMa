using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Jobs;
using RelMa.Application.Abstractions.Services;
using RelMa.Application.UseCases.Sets.V1.Commands;
using RelMa.Application.UseCases.Sets.V1.Queries;
using RelMa.Application.UseCases.Sets.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using RelMa.Shared.Contracts;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class SetEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/sets";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Sets")
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

    public static async Task<PagedResult<SetResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetSetQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:sets",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetSetQuery, PagedResult<SetResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<DefaultIdType> Create(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] CreateSetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateSetCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:sets",
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
        [FromBody] UpdateSetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateSetCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:sets",
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
        [AsParameters] DeleteSetCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteSetCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:sets",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }


    public static async Task<IResult> Upload(
        IFormFile file,
        IJobScheduler scheduler,
        IImageService imageService,
        CancellationToken cancellationToken)
    {
        var fileName = await imageService.SaveImagesAsync(file, cancellationToken);

        await scheduler.TriggerJob(JobContract.GenerateThumbnails, new Dictionary<string, object>
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