using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed record CreatePartCommand(
    string Name,
    string PartNumber,
    string? Category,
    string? Description,
    string? Image,
    int Quantity,
    decimal Cost,
    Ulid[] Items) : ICommand<Ulid>;