using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record CreateAssetCommand(
    string Name,
    string SerialNumber,
    DefaultIdType LocationId,
    string[] Images,
    string? Area,
    string? Barcode,
    string? Category,
    string? Description,
    DefaultIdType? ManufacturerId,
    string? Model) : ICommand<DefaultIdType>;