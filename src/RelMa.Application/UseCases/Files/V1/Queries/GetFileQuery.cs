using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Files.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Files.V1.Queries;

public sealed class GetFileQuery : PaginationQuery, IQuery<PagedResult<FileResponse>>;
