namespace Domain.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

public class User : IdentityUser
{
    [ForeignKey("UserPersonId")]
    public Person UserPerson { get; set; }

    public List<Career> Careers { get; set; } = new();

    public User() { }
    public User(Person userPerson)
    {
        UserPerson = userPerson;
    }
}

