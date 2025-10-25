using Cortex.Mediator.Commands;
using RelMa.Domain.Tasks;
using System.Text.Json;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public sealed record CreateTaskCommand(
    DefaultIdType? AssetId,
    TaskType Type,
    JsonDocument Value) : ICommand<DefaultIdType>;