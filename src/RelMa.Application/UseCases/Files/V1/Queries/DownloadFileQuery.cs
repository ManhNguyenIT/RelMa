using Cortex.Mediator.Queries;
using System.IO.Pipelines;

namespace RelMa.Application.UseCases.Files.V1.Queries;

public sealed record DownloadFileQuery(
    string FileName,
    params DefaultIdType[] Ids) : IQuery<PipeReader>;
