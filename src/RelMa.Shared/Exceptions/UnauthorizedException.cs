namespace RelMa.Shared.Exceptions;

public class UnauthorizedException(string message) : BaseException("Unauthorized", message)
{
}
