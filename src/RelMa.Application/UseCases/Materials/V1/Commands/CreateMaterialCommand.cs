using Cortex.Mediator.Commands;
using RelMa.Domain.Materials;

namespace RelMa.Application.UseCases.Materials.V1.Commands;

public sealed record CreateMaterialCommand(
    string Name,
    string Code,
    Status Status,
    string? Description,
    string[]? Images,
    params DefaultIdType[] Parts) : ICommand<DefaultIdType>;
