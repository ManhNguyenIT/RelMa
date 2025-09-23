using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Maintenances.V1.Responses;
using RelMa.Shared;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Maintenances.V1.Queries;

public sealed class GetMaintenanceQuery : PaginationQuery, IQuery<PagedResult<MaintenanceResponse>>
{
}
