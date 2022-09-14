namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    [ForeignKey("UserPersonId")]
    public Person UserPerson { get; set; }

    public User() { }
    public User(string username, string password, Person userPerson)
    {
        Username = username;
        Password = password;
        UserPerson = userPerson;
    }
}

