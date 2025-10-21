using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public sealed record DeleteWorkOrderCommand(params DefaultIdType[] Ids) : ICommand<bool>;
