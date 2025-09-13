using Cortex.Mediator;
using Microsoft.Extensions.Caching.Distributed;
using RelMa.ApiService.Abstractions;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.UseCases.Users.V1.Commands;
using RelMa.Application.UseCases.Users.V1.Queries;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Infrastructure.Caching;
using RelMa.Shared;

namespace RelMa.ApiService.Endpoints.Users.V1;

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
    }

    public static async Task<IResult> List(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        [AsParameters] GetUserQuery query,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:users",
            param: query,
            factory: async token => await mediator.SendQueryAsync<GetUserQuery, PagedResult<UserResponse>>(query, token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

        return Results.Ok(result);
    }

    public static async Task<IResult> Info(
        IMediator mediator,
        IDistributedCache cache,
        IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var result = await cache.GetOrCreateAsync(
            key: $"{userContext.TenantId}:user-info:{userContext.UserId}",
            param: null,
            factory: async token => await mediator.SendQueryAsync<GetUserInfoQuery, UserResponse>(new GetUserInfoQuery(), token),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5),
            cancellationToken: cancellationToken
        );

        return Results.Ok(result);
    }

    public static async Task<IResult> Sync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<SyncUserCommand, UserResponse>(new SyncUserCommand(), cancellationToken);
        return Results.Ok(result);
    }
}
