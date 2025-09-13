using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public sealed record DeleteStorageCommand(DefaultIdType Id) : ICommand<bool>;
