namespace RelMa.Shared.Exceptions;

public class InvalidException(string message) : BaseException("Invalid request", message)
{
}
