namespace RelMa.Shared.Exceptions;

public class NotFoundException(string message) : BaseException("Not found", message)
{
}
