using Cortex.Mediator;
using Cortex.Mediator.Notifications;
using RelMa.Application.UseCases.Users.V1.Commands;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Shared.Events;

namespace RelMa.Application.UseCases.Users.V1.Events;

public class UserLoggedInHandler(IMediator mediator) : INotificationHandler<UserLoggedInEvent>
{
    public Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
        => mediator.SendCommandAsync<SyncUserCommand, UserResponse>(new SyncUserCommand(), cancellationToken);
}
