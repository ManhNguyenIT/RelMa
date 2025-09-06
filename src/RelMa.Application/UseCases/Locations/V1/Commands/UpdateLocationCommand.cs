using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed record UpdateLocationCommand(Ulid Id, string Name) : ICommand<Ulid>;
