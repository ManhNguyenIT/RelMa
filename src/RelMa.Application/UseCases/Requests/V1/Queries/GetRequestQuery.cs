using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Requests.V1.Queries;

public sealed class GetRequestQuery : PaginationQuery, IQuery<PagedResult<RequestResponse>>;
