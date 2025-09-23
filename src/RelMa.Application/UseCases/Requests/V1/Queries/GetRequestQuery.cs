using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Requests.V1.Queries;

public sealed class GetRequestQuery : PaginationQuery, IQuery<Shared.PagedResult<RequestResponse>>
{
}
