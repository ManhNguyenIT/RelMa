using RelMa.Shared;
using Cortex.Mediator.Queries;
using FluentValidation;
using FluentValidation.Results;

namespace RelMa.Application.Behaviors;

public class ValidationQueryBehavior<TQuery, TResult>(IEnumerable<IValidator<TQuery>> validators) : IQueryPipelineBehavior<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Handle(TQuery query, QueryHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var errors = validators
            .Select(v => v.Validate(query))
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

