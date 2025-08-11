using Cortex.Mediator.Commands;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RelMa.Application.Behaviors;

public class TracingCommandBehavior<TCommand, TResult>(ILogger<TracingCommandBehavior<TCommand, TResult>> logger) : ICommandPipelineBehavior<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CommandHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {

        var stopwatch = Stopwatch.StartNew();

        var result = await next();

        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        var commandName = typeof(TCommand).Name;

        if (elapsed > 3000)
        {
            logger.LogWarning("Command details: {Name} ({ElapsedMilliseconds}ms) {@Command}",
                commandName, elapsed, command);
        }
        else
        {
            logger.LogInformation("Command details: {Name} ({ElapsedMilliseconds}ms) {@Command}",
                commandName, elapsed, command);
        }

        return result;
    }
}