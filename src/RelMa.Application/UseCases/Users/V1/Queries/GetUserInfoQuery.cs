using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Users.V1.Responses;

namespace RelMa.Application.UseCases.Users.V1.Queries;

public sealed record GetUserInfoQuery : IQuery<UserResponse>;
