using Microsoft.EntityFrameworkCore.Storage;
using RelMa.Shared.Abstractions.Entity;
using System.Data;

namespace RelMa.Application.Abstractions.Database;
public interface IUnitOfWork : IAsyncDisposable
{
    IDbConnection Connection { get; }
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TKey : notnull
        where TEntity : class, IEntity<TKey>;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
