namespace RelMa.Shared.Exceptions;

public class BadRequestException(string message) : BaseException("Bad request", message)
{
}
