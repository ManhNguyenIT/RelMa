using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Materials.V1.Commands;

public sealed record DeleteMaterialCommand(params DefaultIdType[] Ids) : ICommand<bool>;
