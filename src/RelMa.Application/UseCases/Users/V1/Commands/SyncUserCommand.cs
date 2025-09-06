using Cortex.Mediator.Commands;
using RelMa.Application.UseCases.Users.V1.Responses;

namespace RelMa.Application.UseCases.Users.V1.Commands;

public sealed record SyncUserCommand() : ICommand<UserResponse>;
