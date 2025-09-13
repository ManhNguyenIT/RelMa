using Cortex.Mediator;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;

public sealed class DomainEventsInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData?.Context is null)
            return result;

        var provider = eventData.Context?.GetService<IServiceProvider>()
            ?? throw new InvalidOperationException("Unable to resolve IServiceProvider from Context in SaveChangesInterceptor.");
        var mediator = provider.GetRequiredService<IMediator>();

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