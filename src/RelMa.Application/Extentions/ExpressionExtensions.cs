using Microsoft.EntityFrameworkCore;
using RelMa.Shared;
using System.Linq.Expressions;

namespace RelMa.Application.Extentions;

public static class ExpressionExtensions
{
    public static IQueryable<T> Includes<T>(this IQueryable<T> source, IEnumerable<string>? includes) where T : class
    {
        foreach (var include in includes ?? [])
        {
            var includeExpression = BuildIncludeExpression<T>(include);
            source = source.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(EntityFrameworkQueryableExtensions),
                    "Include",
                    [typeof(T), includeExpression.Body.Type],
                    source.Expression,
                    Expression.Quote(includeExpression)
                )
            );
        }

        return source;
    }

    public static IQueryable<T> Select<T>(this IQueryable<T> source, IEnumerable<string>? columns)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var bindings = new List<MemberBinding>();

        foreach (var column in columns ?? [])
        {
            var memberExpression = GetMemberExpression(param, column);

            var binding = Expression.Bind(memberExpression.Member, memberExpression);
            bindings.Add(binding);
        }

        var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);

        var selector = Expression.Lambda<Func<T, T>>(body, param);

        return source.Select(selector);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page.HasValue && pageSize.HasValue)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page.Value,
                PageSize = pageSize.Value,
                RowCount = await query.CountAsync(cancellationToken)
            };
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / pageSize.Value);
            result.Items = await query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToArrayAsync(cancellationToken);
            return result;
        }
        else
        {
            var items = await query.ToArrayAsync(cancellationToken);
            return new PagedResult<T>
            {
                Items = items,
                RowCount = items.Length,
                PageSize = 1,
                PageCount = 1,
                CurrentPage = 1,
            };
        }
    }

    private static LambdaExpression BuildIncludeExpression<T>(string include) where T : class
    {
        var param = Expression.Parameter(typeof(T), "x");
        Expression body = param;

        foreach (var member in include.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }

        return Expression.Lambda(body, param);
    }

    private static MemberExpression GetMemberExpression(Expression param, string propertyName)
    {
        string[] properties = propertyName.Split('.');
        Expression member = param;

        foreach (string prop in properties)
        {
            member = Expression.Property(member, prop)
                ?? throw new ArgumentException($"Property {prop} does not exist in type {member.Type.Name}");
        }

        return member is not MemberExpression memberExpression
            ? throw new InvalidOperationException($"Expression for property {propertyName} is not a MemberExpression.")
            : memberExpression;
    }
}
