using Domain.Entities.PersonContainer;
using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Entities.UserContianer;

public class User
{
    public User(string username, string password, Person p)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(username));
        UserPerson = p ?? throw new ArgumentNullException(nameof(p));
    }
    public string Username { get; set; }
    public string Password { get; set; }
    public Person UserPerson { get; set; }
}

