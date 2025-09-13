using Microsoft.EntityFrameworkCore.Diagnostics;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;

public sealed class TenantEntityInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData?.Context is null || string.IsNullOrEmpty(userContext.TenantId))
            return ValueTask.FromResult(result);

        foreach (var entry in eventData.Context?.ChangeTracker.Entries<ITenantTracking>() ?? [])
        {
            entry.Entity.TenantId = userContext.TenantId;
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}
