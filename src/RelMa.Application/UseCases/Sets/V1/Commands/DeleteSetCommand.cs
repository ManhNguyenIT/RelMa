using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public sealed record DeleteSetCommand(Ulid Id) : ICommand<bool>;
