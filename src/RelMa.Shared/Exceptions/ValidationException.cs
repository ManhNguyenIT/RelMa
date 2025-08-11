namespace RelMa.Shared.Exceptions;

public sealed class ValidationException(IReadOnlyCollection<ValidationError> errors) : BaseException("Validation failure", "One or more validation errors occurred")
{
    public IReadOnlyCollection<ValidationError> Errors { get; } = errors;
}

public record ValidationError(string PropertyName, string ErrorMessage);
