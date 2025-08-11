using Microsoft.EntityFrameworkCore.Query;
using RelMa.Shared.Abstractions.Entity;
using System.Linq.Expressions;

namespace RelMa.Application.Abstractions.Database;
public interface IRepository<TEntity, in TKey> : IAsyncDisposable
    where TEntity : IEntity<TKey>
{
    IQueryable<TEntity> Find(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null);
    Task<TEntity?> FindByIdAsync(
        TKey id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        CancellationToken cancellationToken = default);
    Task<TEntity?> FindSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        CancellationToken cancellationToken = default);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveMultiple(IEnumerable<TEntity> entities);
}
