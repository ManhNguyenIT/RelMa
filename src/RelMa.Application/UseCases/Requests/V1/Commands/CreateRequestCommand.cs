using Cortex.Mediator.Commands;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed record CreateRequestCommand(
    DefaultIdType AssetId,
    string Title,
    string? Description,
    Priority Priority,
    string? Image,
    Status Status) : ICommand<DefaultIdType>;