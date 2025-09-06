using Cortex.Mediator;
using RelMa.ApiService.Abstractions;
using RelMa.Application.UseCases.Users.V1.Commands;
using RelMa.Application.UseCases.Users.V1.Responses;

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

        route.MapPost("sync", Sync)
            .RequireAuthorization();
    }


    public static async Task<IResult> Sync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<SyncUserCommand, UserResponse>(new SyncUserCommand(), cancellationToken);
        return Results.Ok(result);
    }

}
