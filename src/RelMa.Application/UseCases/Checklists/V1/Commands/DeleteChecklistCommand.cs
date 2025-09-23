using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed record DeleteChecklistCommand(DefaultIdType Id) : ICommand<bool>;
