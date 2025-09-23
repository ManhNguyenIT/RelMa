using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed record UpdateMaintenanceCommand(
    DefaultIdType Id,
    DefaultIdType WorkOrderId,
    string CronExpression,
    params string[]? Images) : ICommand<DefaultIdType>;