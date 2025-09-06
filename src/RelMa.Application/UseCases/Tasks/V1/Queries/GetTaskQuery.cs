using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Tasks.V1.Queries;

public sealed class GetTaskQuery : PaginationQuery, IQuery<PagedResult<TaskResponse>>;
