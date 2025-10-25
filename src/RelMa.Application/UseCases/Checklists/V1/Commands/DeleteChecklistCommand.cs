using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed record DeleteChecklistCommand(params DefaultIdType[] Ids) : ICommand<bool>;
