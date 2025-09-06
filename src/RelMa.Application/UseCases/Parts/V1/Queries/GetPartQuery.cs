using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Parts.V1.Queries;

public sealed class GetPartQuery : PaginationQuery, IQuery<PagedResult<PartResponse>>;
