using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed record RejectRequestCommand(DefaultIdType Id) : ICommand<DefaultIdType>;
