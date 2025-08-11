using Cortex.Mediator.Commands;
using FluentValidation;
using FluentValidation.Results;
using RelMa.Shared;

namespace RelMa.Application.Behaviors;

public class ValidationCommandBehavior<TCommand, TResult>(IEnumerable<IValidator<TCommand>> validators) : ICommandPipelineBehavior<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CommandHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var errors = validators
            .Select(v => v.Validate(command))
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .Select(e => new Error(e.PropertyName, e.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length == 0)
        {
            return await next();
        }
        else
        {
            throw new ValidationException("Validation failed", errors.Select(e => new ValidationFailure(e.Code, e.Message)));
        }
    }
}