using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public sealed record UpdateSetCommand(
    Ulid Id,
    string Name,
    Ulid[] Parts) : ICommand<Ulid>;
