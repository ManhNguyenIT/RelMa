using Cortex.Mediator.Commands;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed record CreateRequestCommand(
    DefaultIdType AssetId,
    string Title,
    string? Description,
    Status Status,
    Category Category,
    Priority Priority,
    string[]? Images) : ICommand<DefaultIdType>;