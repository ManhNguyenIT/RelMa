using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database.Interceptors;
public sealed class AuditableEntityInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
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
