using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Checklists.V1.Responses;
using RelMa.Shared;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Checklists.V1.Queries;

public sealed class GetChecklistQuery : PaginationQuery, IQuery<PagedResult<ChecklistResponse>>;
