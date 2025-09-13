using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Locations.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Locations.V1.Queries;

public sealed class GetLocationQuery : PaginationQuery, IQuery<Shared.PagedResult<LocationResponse>>;
