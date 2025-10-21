using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed record UpdateTeamCommand(
    DefaultIdType Id,
    string Name,
    string? Description,
    DefaultIdType LeaderId,
    params DefaultIdType[] MemberIds) : ICommand<DefaultIdType>;