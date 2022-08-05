using Domain;
using Domain.Entities;

try
{
    HumanManager p = new HumanManager("SlimFiFi01","A7vcsdv$","nour", "2001-02-18", "Egyptian", "D:\\Stuff\\Work-stuff\\Project\\FootballManagerDomainModel.png");
    Team rm = new Team("RMA", "Spain", "Santiago bernabeu", p);

    Console.WriteLine(rm.Name + "\n" + rm.Manager);
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

Console.ReadKey();