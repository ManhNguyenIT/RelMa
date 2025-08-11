using Cortex.Mediator.Commands;
using Microsoft.Extensions.Logging;
using RelMa.Application.Abstractions.Database;

namespace RelMa.Application.Behaviors;

public class TransactionCommandBehavior<TCommand, TResult>(IUnitOfWork unitOfWork, ILogger<TransactionCommandBehavior<TCommand, TResult>> logger) : ICommandPipelineBehavior<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CommandHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Begin transaction for command {CommandType}", typeof(TCommand).Name);

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await next();

            await unitOfWork.CommitAsync(cancellationToken);

            logger.LogInformation("Commit transaction for command {CommandType}", typeof(TCommand).Name);

            return result;
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
