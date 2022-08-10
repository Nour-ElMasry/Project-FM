using Domain.Exceptions;
using System;
using System.IO;

namespace Domain.Entities.PersonContainer;

public abstract class Person
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public string Photo { get; set; }

    public Person(string name, string birthDate, string nationality, string photo)
    {
        if (!DateTime.TryParse(birthDate, out DateTime tempDate))
            throw new IncorrectDateException("Invalid date! please input a correct date!");

        if (!File.Exists(photo))
            throw new FileNotFoundException("File doesn't exist");

        Name = name;
        BirthDate = tempDate;
        Nationality = nationality;
        Photo = photo;
    }
}

