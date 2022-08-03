
namespace Domain.Exceptions;

public class PasswordNotSecureException : Exception
{
    public PasswordNotSecureException(string? message) : base(message)
    {
    }
}

