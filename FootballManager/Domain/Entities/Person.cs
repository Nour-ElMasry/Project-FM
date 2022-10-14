using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Person
{
    [Key]
    public long PersonId { get; set; }
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Country { get; set; }
    public string Image { get; set; } = "https://www.thestreet.com/.image/t_share/MTgzNTYwNDE0NDE0NTEzMTkw/screen-shot-2021-09-01-at-85022-am.png";

    public Person() { }
    public Person(string name, string birthDate, string country)
    {
        if (birthDate == null)
        {
            BirthDate = new DateTime(2001, 02, 16);
        }

        if (BirthDate == null)
        {
            if (!DateTime.TryParse(birthDate, out DateTime tempDate))
                throw new IncorrectDateException("Invalid date! please input a correct date!");

            BirthDate = tempDate;
        }

        Name = name;
        Country = country;
    }
}

