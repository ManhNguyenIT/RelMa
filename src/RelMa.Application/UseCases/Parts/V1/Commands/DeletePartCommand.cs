using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed record DeletePartCommand(params DefaultIdType[] Ids) : ICommand<bool>;
