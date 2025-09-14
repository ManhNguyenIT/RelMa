using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.Manufacturers.V1.Commands;
using RelMa.Application.UseCases.Manufacturers.V1.Queries;
using RelMa.Application.UseCases.Manufacturers.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class ManufacturerEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/manufacturers";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Manufacturers")
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
    }

    public static async Task<IResult> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetManufacturerQuery query,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:manufacturers",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetManufacturerQuery, PagedResult<ManufacturerResponse>>(query, token),
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
        [FromBody] CreateManufacturerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateManufacturerCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:manufacturers",
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
        [FromBody] UpdateManufacturerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateManufacturerCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:manufacturers",
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
        [FromBody] DeleteManufacturerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteManufacturerCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:manufacturers",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return Results.Ok(result);
    }

}
