using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record UpdateAssetCommand(
    Ulid Id,
    string Name,
    string? Description,
    string? Model,
    Ulid LocationId,
    Ulid? ManufacturerId,
    string SerialNumber,
    string? Category,
    string? Area,
    string? Barcode) : ICommand<Ulid>;
