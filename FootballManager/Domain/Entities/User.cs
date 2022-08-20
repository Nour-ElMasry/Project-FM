namespace Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Person UserPerson { get; set; }

    public User(string username, string password, Person userPerson)
    {
        Username = username;
        Password = password;
        UserPerson = userPerson;
    }
}

