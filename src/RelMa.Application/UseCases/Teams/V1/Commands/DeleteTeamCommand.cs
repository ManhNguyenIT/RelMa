using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed record DeleteTeamCommand(params DefaultIdType[] Ids) : ICommand<bool>;