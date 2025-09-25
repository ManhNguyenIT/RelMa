using Cortex.Mediator.Queries;

namespace RelMa.Application.UseCases.Files.V1.Queries;

public sealed record DownloadFileQuery(
    string FileName,
    params DefaultIdType[] Ids) : IQuery<MemoryStream>;
