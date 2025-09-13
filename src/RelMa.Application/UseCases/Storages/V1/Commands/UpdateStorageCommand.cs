using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public sealed record UpdateStorageCommand(DefaultIdType Id, string Name) : ICommand<DefaultIdType>;
