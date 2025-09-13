using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed record CreateLocationCommand(string Name) : ICommand<DefaultIdType>;