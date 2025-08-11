using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RelMa.Application.Abstractions.Database;
using System.Linq.Expressions;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Infrastructure.Database;
public sealed class Repository<TEntity, TKey>(ApplicationDbContext context) : IRepository<TEntity, TKey>
    where TKey : notnull
    where TEntity : class, IEntity<TKey>
{
    public IQueryable<TEntity> Find(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (include is not null)
        {
            query = include(query);
        }
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public async Task<TEntity?> FindByIdAsync(
        TKey id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        CancellationToken cancellationToken = default)
        => await Find(null, include)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity?> FindSingleAsync(
        Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object?>>? include = null,
        CancellationToken cancellationToken = default)
        => await Find(null, include)
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate, cancellationToken);

    public void Add(TEntity entity)
        => context.Add(entity);

    public void Update(TEntity entity)
        => context.Update(entity);

    public void Remove(TEntity entity)
        => context.Remove(entity);

    public void RemoveMultiple(IEnumerable<TEntity> entities)
        => context.RemoveRange(entities);

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await context.DisposeAsync();
}
