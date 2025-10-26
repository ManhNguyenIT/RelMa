using Cortex.Mediator.Commands;
using Microsoft.AspNetCore.Http;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed record ImportAssetCommand(IFormFile File) : ICommand<(DefaultIdType, string)>;