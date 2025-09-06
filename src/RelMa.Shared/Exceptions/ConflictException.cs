namespace RelMa.Shared.Exceptions;

public class ConflictException(string message) : BaseException("Conflict", message)
{
}
