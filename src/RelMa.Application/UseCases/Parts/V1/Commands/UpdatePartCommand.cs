using Cortex.Mediator.Commands;
using RelMa.Domain.Parts;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed record UpdatePartCommand(
    DefaultIdType Id,
    DefaultIdType MaterialId,
    int? Minimum,
    int? Quantity,
    int? Inventory,
    decimal? Cost,
    string? Category,
    string? Description,
    Status? Status,
    DefaultIdType? StorageId,
    DefaultIdType? LocationId) : ICommand<DefaultIdType>;
