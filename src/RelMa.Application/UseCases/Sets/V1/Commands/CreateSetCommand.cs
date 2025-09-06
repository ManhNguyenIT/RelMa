using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public sealed record CreateSetCommand(
    string Name,
    Ulid[] Parts) : ICommand<Ulid>;