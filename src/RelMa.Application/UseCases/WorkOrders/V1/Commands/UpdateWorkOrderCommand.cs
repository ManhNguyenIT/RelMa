using Cortex.Mediator.Commands;
using RelMa.Domain.WorkOrders;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public sealed record UpdateWorkOrderCommand(
    DefaultIdType Id,
    DefaultIdType AssetId,
    string Title,
    string? Description,
    Status Status,
    Category Category,
    Priority Priority,
    string[]? Images) : ICommand<DefaultIdType>;
