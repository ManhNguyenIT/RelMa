using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;
using RelMa.ApiService.Abstractions;
using RelMa.Application.UseCases.Files.V1.Commands;
using RelMa.Application.UseCases.Files.V1.Queries;
using System.IO.Pipelines;

namespace RelMa.ApiService.Endpoints.V1;

internal sealed class FileEndpoint : IEndpoint
{
    private const string BaseUrl = "/api/v{version:apiVersion}/files";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var route = app.NewVersionedApi()
            .WithTags("Files")
            .MapGroup(BaseUrl)
            .HasApiVersion(1.0);

        route.MapPost("upload", Upload)
            .RequireAuthorization()
            .DisableAntiforgery();

        route.MapGet("prepare-download", Download)
            .RequireAuthorization();

        route.MapGet("download", Download)
            .RequireAuthorization();
    }

    private static async Task<IResult> Upload(
        IMediator mediator,
        [FromForm] UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendCommandAsync<UploadFileCommand, IEnumerable<DefaultIdType>>(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Download(
        IMediator mediator,
        [AsParameters] DownloadFileQuery query,
        CancellationToken cancellationToken)
    {
        var reader = await mediator.SendQueryAsync<DownloadFileQuery, PipeReader>(query, cancellationToken);
        return Results.File(reader.AsStream(), "application/octet-stream", query.FileName);
    }
}