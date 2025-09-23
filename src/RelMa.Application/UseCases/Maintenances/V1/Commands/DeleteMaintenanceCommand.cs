using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed record DeleteMaintenanceCommand(DefaultIdType Id) : ICommand<bool>;
