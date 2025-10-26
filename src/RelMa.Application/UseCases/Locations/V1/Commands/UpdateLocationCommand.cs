using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed record UpdateLocationCommand(
    DefaultIdType Id,
    string Name,
    string? Description) : ICommand<DefaultIdType>;
