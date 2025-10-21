using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public sealed record DeleteStorageCommand(params DefaultIdType[] Ids) : ICommand<bool>;
