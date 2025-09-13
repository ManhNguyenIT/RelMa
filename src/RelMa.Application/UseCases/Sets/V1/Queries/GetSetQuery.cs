using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Sets.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Sets.V1.Queries;

public sealed class GetSetQuery : PaginationQuery, IQuery<Shared.PagedResult<SetResponse>>;
