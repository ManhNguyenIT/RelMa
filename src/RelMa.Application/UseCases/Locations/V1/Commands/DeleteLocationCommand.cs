using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed record DeleteLocationCommand(Ulid Id) : ICommand<bool>;
