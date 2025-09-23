using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Jobs;
using RelMa.Application.Abstractions.Services;
using RelMa.Application.UseCases.Checklists.V1.Commands;
using RelMa.Application.UseCases.Checklists.V1.Queries;
using RelMa.Application.UseCases.Checklists.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class ChecklistEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/checklists";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Checklists")
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
        [AsParameters] GetChecklistQuery query,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:checklists",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetChecklistQuery, PagedResult<ChecklistResponse>>(query, token),
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
        [FromBody] CreateChecklistCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateChecklistCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:checklists",
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
        [FromBody] UpdateChecklistCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateChecklistCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:checklists",
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
        [FromBody] DeleteChecklistCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteChecklistCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:checklists",
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

    public static async Task<IResult> Export()
    {
        await Task.CompletedTask;
        using var stream = new MemoryStream();
        return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "checklists_exported.xlsx");
    }

    public static async Task<IResult> Template()
    {
        await Task.CompletedTask;
        using var stream = new MemoryStream();
        return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "checklists_template.xlsx");
    }
}
