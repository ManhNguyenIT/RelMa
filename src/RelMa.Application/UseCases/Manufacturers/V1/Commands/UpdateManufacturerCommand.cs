using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public sealed record UpdateManufacturerCommand(DefaultIdType Id, string Name) : ICommand<DefaultIdType>;
