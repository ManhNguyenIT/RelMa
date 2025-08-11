using Cortex.Mediator.Queries;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RelMa.Application.Behaviors;

public class TracingQueryBehavior<TQuery, TResult>(ILogger<TQuery> logger) : IQueryPipelineBehavior<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Handle(TQuery query, QueryHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {

        var stopwatch = Stopwatch.StartNew();

        var result = await next();

        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        var queryName = typeof(TQuery).Name;

        if (elapsed > 3000)
        {
            logger.LogWarning("Query details: {Name} ({ElapsedMilliseconds}ms) {@Query}",
                queryName, elapsed, query);
        }
        else
        {
            logger.LogInformation("Query details: {Name} ({ElapsedMilliseconds}ms) {@Query}",
                queryName, elapsed, query);
        }

        return result;
    }
}
