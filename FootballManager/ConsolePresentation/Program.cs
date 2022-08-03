using Domain;
using Domain.Entities;

try
{
    User m = new("SlimFifi01", "A7fguyqw?eu12312", "nour", "2001-02-18", "Egyptian", "D:\\Stuff\\Work-stuff\\Project\\FootballManagerDomainModel.png");
    HumanManager m1 = new(m, new Team("Real Madrid", "Spain", "Santiago Bernabeu"));

    Console.WriteLine(m1.CurrentUser.ToString());
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

Console.ReadKey();