﻿using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Person
{
    [Key]
    public long PersonId { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }

    public Person() { }
    public Person(string name, string birthDate, string country)
    {
        if (!DateTime.TryParse(birthDate, out DateTime tempDate))
            throw new IncorrectDateException("Invalid date! please input a correct date!");
        
        if (DateTime.Now.Year - 5 <= tempDate.Year)
            throw new IncorrectDateException("Invalid date! please input a correct date!");

        Name = name;
        BirthDate = tempDate;
        Country = country;
    }
}

