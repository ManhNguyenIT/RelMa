using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Users.V1.Commands;

public sealed record DeleteUserCommand(params DefaultIdType[] Ids) : ICommand<bool>;