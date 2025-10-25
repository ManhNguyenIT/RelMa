using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.WorkOrders.V1.Commands;
using RelMa.Application.UseCases.WorkOrders.V1.Queries;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class WorkOrderEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/work-orders";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("WorkOrders")
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

        route.MapPost("import", Import)
            .RequireAuthorization();

        route.MapGet("export", Export)
            .RequireAuthorization();

        route.MapGet("template", Template)
            .RequireAuthorization();
    }

    public static async Task<PagedResult<WorkOrderResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetWorkOrderQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:work-orders",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetWorkOrderQuery, PagedResult<WorkOrderResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<DefaultIdType> Create(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] CreateWorkOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateWorkOrderCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:work-orders",
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
        [FromBody] UpdateWorkOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateWorkOrderCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:work-orders",
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
        [AsParameters] DeleteWorkOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteWorkOrderCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:work-orders",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
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
        return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "work-orders_exported.xlsx");
    }

    public static async Task<IResult> Template()
    {
        await Task.CompletedTask;
        using var stream = new MemoryStream();
        return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "work-orders_template.xlsx");
    }
}
