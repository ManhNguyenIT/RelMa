using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record UpdateAssetCommand(
    DefaultIdType Id,
    string Name,
    string? Description,
    string? Model,
    DefaultIdType LocationId,
    DefaultIdType? ManufacturerId,
    string SerialNumber,
    string? Category,
    string? Area,
    string? Barcode) : ICommand<DefaultIdType>;
