using Cortex.Mediator;
using RelMa.ApiService.Abstractions;
using RelMa.Application.UseCases.Users.V1.Commands;
using RelMa.Application.UseCases.Users.V1.Responses;

namespace RelMa.ApiService.Endpoints.WorkOrders.V1;

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
    }


    public static async Task<IResult> List(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<SyncUserCommand, UserResponse>(new SyncUserCommand(), cancellationToken);
        return Results.Ok(result);
    }

}
