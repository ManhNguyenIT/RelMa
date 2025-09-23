using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed record CreateMaintenanceCommand(
    DefaultIdType WorkOrderId,
    string CronExpression,
    params string[]? Images) : ICommand<DefaultIdType>;
