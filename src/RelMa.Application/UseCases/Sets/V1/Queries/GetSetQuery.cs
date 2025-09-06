using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Sets.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Sets.V1.Queries;

public sealed class GetSetQuery : PaginationQuery, IQuery<PagedResult<SetResponse>>;
