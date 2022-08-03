using Domain;
using Domain.Entities;

try
{
    Person m = new User("SlimFifi01", "A7fguyqwe?u12312", "nour", "2001-02-18", "Egyptian", "D:\\Stuff\\Work-stuff\\Project\\FootballManagerDomainModel.png");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

Console.ReadKey();