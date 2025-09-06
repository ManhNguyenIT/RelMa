using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.WorkOrders.V1.Queries;

public sealed class GetWorkOrderQuery : PaginationQuery, IQuery<PagedResult<WorkOrderResponse>>;
