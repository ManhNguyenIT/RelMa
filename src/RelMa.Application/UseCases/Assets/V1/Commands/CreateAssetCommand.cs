using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record CreateAssetCommand(
    string Name,
    string Code,
    DefaultIdType LocationId,
    string[] Images,
    string? Area,
    string? Category,
    string? Description,
    string? SerialNumber,
    DefaultIdType? ManufacturerId,
    string? Model) : ICommand<DefaultIdType>;