namespace Domain.Exceptions;

public class IncorrectDateException : Exception
{
    public IncorrectDateException(string? message) : base(message)
    {
    }
}

