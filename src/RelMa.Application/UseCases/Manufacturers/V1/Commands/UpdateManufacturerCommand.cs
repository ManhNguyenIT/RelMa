using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public sealed record UpdateManufacturerCommand(Ulid Id, string Name) : ICommand<Ulid>;
