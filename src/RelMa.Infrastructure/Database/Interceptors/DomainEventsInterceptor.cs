using Cortex.Mediator;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;

public sealed class DomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context?.ChangeTracker.Entries<IAggregateRoot>();

        var tasks = entries?
            .SelectMany(e =>
            {
                var events = e.Entity.Events.ToList();
                e.Entity.ClearEvents();
                return events;
            })
            .Select(@event => (Task)mediator.PublishAsync((dynamic)@event, cancellationToken));

        if (tasks is not null)
        {
            await Task.WhenAll(tasks);
        }

        return result;
    }
}