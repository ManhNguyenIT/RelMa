using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RelMa.Application.Abstractions.Database;
using RelMa.Shared.Abstractions.Entity;
using System.Collections.Concurrent;
using System.Data;

namespace RelMa.Infrastructure.Database;
internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public IDbConnection Connection => context.Database.GetDbConnection();

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TKey : notnull
        where TEntity : class, IEntity<TKey>
        => (IRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity), _ => new Repository<TEntity, TKey>(context));

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => context.Database.CurrentTransaction ?? await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await context.Database.CommitTransactionAsync(cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        context.ChangeTracker.Clear();
        return context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await context.DisposeAsync();
}
