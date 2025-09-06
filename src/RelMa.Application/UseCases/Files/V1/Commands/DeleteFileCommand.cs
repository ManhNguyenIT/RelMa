using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Files.V1.Commands;

public sealed record DeleteFileCommand(params Ulid[] Ids) : ICommand<bool>;