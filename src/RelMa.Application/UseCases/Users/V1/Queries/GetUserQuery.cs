using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Users.V1.Queries;

public sealed class GetUserQuery : PaginationQuery, IQuery<PagedResult<UserResponse>>;
