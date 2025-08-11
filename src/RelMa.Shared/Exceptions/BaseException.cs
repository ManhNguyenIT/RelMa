namespace RelMa.Shared.Exceptions;

public class BaseException(string title, string message) : Exception(message)
{
    public string Title { get; } = title;
}
