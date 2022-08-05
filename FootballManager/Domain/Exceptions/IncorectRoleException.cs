namespace Domain.Exceptions;
public class IncorectRoleException : Exception
{
    public IncorectRoleException(string? message) : base(message)
    {
    }
}

