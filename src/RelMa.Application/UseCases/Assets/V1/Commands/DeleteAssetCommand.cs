using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record DeleteAssetCommand(params DefaultIdType[] Ids) : ICommand<bool>;
