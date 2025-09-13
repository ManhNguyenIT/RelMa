using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Tasks.V1.Queries;

public sealed class GetTaskQuery : PaginationQuery, IQuery<Shared.PagedResult<TaskResponse>>;
