using Cortex.Mediator.Commands;
using RelMa.Domain.WorkOrders;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public sealed record CreateWorkOrderCommand(
    string Title,
    string? Description,
    string? Note,
    Status Status,
    Category Category,
    Priority Priority,
    TimeSpan? Estimate,
    DefaultIdType? RequestId,
    DefaultIdType? MaintenanceId,
    string[]? Images,
    DefaultIdType? AssigneeId,
    DefaultIdType[] Files) : ICommand<DefaultIdType>;