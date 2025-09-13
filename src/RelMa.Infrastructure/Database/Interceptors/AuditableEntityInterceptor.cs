using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData?.Context is null)
            return new ValueTask<InterceptionResult<int>>(result);

        var provider = eventData.Context?.GetService<IServiceProvider>()
            ?? throw new InvalidOperationException("Unable to resolve IServiceProvider from Context in SaveChangesInterceptor.");
        var userContext = provider.GetRequiredService<IUserContext>();

        foreach (var entry in eventData.Context?.ChangeTracker.Entries<IAuditable>() ?? [])
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = userContext.UserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
                    entry.Entity.ModifiedBy = userContext.UserId;
                    break;
            }
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}
