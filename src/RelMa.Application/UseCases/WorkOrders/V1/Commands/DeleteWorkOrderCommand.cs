using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public sealed record DeleteWorkOrderCommand(DefaultIdType Id) : ICommand<bool>;
