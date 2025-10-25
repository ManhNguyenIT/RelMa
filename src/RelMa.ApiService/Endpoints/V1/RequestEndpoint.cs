using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.Requests.V1.Commands;
using RelMa.Application.UseCases.Requests.V1.Queries;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class RequestEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/requests";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Requests")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapGet(string.Empty, List)
            .AllowAnonymous();

        route.MapPost(string.Empty, Create)
            .AllowAnonymous();

        route.MapPut(string.Empty, Update)
            .RequireAuthorization();

        route.MapDelete(string.Empty, Delete)
            .RequireAuthorization();
    }

    public static async Task<PagedResult<RequestResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetRequestQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:requests",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetRequestQuery, PagedResult<RequestResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<DefaultIdType> Create(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [FromBody] CreateRequestCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<CreateRequestCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:requests",
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
        [FromBody] UpdateRequestCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UpdateRequestCommand, DefaultIdType>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:requests",
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
        [AsParameters] DeleteRequestCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteRequestCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:requests",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

}