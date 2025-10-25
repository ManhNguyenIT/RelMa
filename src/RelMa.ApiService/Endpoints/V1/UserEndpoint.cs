using Cortex.Mediator;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.Users.V1.Commands;
using RelMa.Application.UseCases.Users.V1.Queries;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared;
using StackExchange.Redis;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class UserEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/users";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Users")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapGet(string.Empty, List)
            .RequireAuthorization();

        route.MapGet("info", Info)
            .RequireAuthorization();

        route.MapPost("sync", Sync)
            .RequireAuthorization();

        route.MapDelete(string.Empty, Delete)
            .RequireAuthorization();

    }

    public static async Task<PagedResult<UserResponse>?> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetUserQuery query,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:users",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetUserQuery, PagedResult<UserResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<UserResponse?> Info(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:user-info:{userContext.UserId}",
            param: null,
            factory: async token => await mediator.SendQueryAsync<GetUserInfoQuery, UserResponse>(new GetUserInfoQuery(), token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

    public static async Task<UserResponse?> Sync(
        IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.SendCommandAsync<SyncUserCommand, UserResponse>(new SyncUserCommand(), cancellationToken);


    public static async Task<bool> Delete(
        IMediator mediator,
        IUserContext userContext,
        IDistributedCache cache,
        IConnectionMultiplexer multiplexer,
        IOptions<RedisCacheOptions> options,
        [AsParameters] DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<DeleteUserCommand, bool>(command, cancellationToken);
        string[] patterns =
        [
            $"{userContext.TenantId}:users",
        ];
        await cache.RemoveCachesAsync(multiplexer, options, patterns, cancellationToken);
        return result;
    }

}
