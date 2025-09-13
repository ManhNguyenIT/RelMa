using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public sealed record UpdateSetCommand(
    DefaultIdType Id,
    string Name,
    DefaultIdType[] Parts) : ICommand<DefaultIdType>;
