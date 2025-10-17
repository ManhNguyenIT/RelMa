using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Teams.V1.Responses;
using RelMa.Shared;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Teams.V1.Queries;

public sealed class GetTeamQuery : PaginationQuery, IQuery<PagedResult<TeamResponse>>;
