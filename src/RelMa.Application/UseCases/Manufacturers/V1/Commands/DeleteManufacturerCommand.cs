using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public sealed record DeleteManufacturerCommand(DefaultIdType Id) : ICommand<bool>;
