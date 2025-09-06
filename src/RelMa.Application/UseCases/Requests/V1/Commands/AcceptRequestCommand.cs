using Cortex.Mediator.Commands;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed record AcceptRequestCommand(Ulid Id) : ICommand<Ulid>;
