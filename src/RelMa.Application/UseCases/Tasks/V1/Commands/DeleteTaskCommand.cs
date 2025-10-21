using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public sealed record DeleteTaskCommand(params DefaultIdType[] Ids) : ICommand<bool>;
