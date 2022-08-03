namespace Domain.Exceptions;
public class IncorrectPositionException : Exception
{
    public IncorrectPositionException(string? message) : base(message)
    {
    }
}
