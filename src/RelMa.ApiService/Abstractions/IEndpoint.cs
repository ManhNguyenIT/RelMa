namespace RelMa.ApiService.Abstractions;

internal interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
