using Cortex.Mediator.Commands;
using Microsoft.AspNetCore.Http;

namespace RelMa.Application.UseCases.Files.V1.Commands;

public sealed record UploadFileCommand(IFormFileCollection Files) : ICommand<IEnumerable<Ulid>>;