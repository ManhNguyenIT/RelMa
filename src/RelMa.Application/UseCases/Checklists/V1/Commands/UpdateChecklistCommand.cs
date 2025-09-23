using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed record UpdateChecklistCommand(
    DefaultIdType Id,
    string Name,
    string? Description,
    DefaultIdType WorkOrderId,
    params DefaultIdType[] Tasks) : ICommand<DefaultIdType>;
