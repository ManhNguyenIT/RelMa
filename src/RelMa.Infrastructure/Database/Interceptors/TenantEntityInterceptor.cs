using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;

public class TenantEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData?.Context is null)
            return new ValueTask<InterceptionResult<int>>(result);

        var provider = eventData.Context?.GetService<IServiceProvider>()
            ?? throw new InvalidOperationException("Unable to resolve IServiceProvider from Context in SaveChangesInterceptor.");
        var userContext = provider.GetRequiredService<IUserContext>();

        foreach (var entry in eventData.Context?.ChangeTracker.Entries<ITenantTracking>() ?? [])
        {
            entry.Entity.TenantId = userContext.TenantId;
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}
