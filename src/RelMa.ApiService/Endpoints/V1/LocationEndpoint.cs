using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.Locations.V1.Commands;
using RelMa.Application.UseCases.Locations.V1.Queries;
using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class LocationEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/locations";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Locations")
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

    public static async Task<PagedResult<LocationResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetLocationQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:locations",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetLocationQuery, PagedResult<LocationResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<DefaultIdType> Create(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateLocationCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:locations",
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
        [FromBody] UpdateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateLocationCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:locations",
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
        [AsParameters] DeleteLocationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteLocationCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:locations",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

}
