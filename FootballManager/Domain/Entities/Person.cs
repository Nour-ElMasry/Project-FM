using Domain.Exceptions;
using System;
using System.IO;

namespace Domain.Entities;

public abstract class Person
{
    public long PersonId { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }
    public string Photo { get; set; }

    public Person(string name, string birthDate, string country, string photo)
    {
        if (!DateTime.TryParse(birthDate, out DateTime tempDate))
            throw new IncorrectDateException("Invalid date! please input a correct date!");

        if (!File.Exists(photo))
            throw new FileNotFoundException("File doesn't exist");

        Name = name;
        BirthDate = tempDate;
        Country = country;
        Photo = photo;
    }
}

