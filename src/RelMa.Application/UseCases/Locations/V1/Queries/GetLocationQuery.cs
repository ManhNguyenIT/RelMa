using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Locations.V1.Queries;

public sealed class GetLocationQuery : PaginationQuery, IQuery<PagedResult<LocationResponse>>;
