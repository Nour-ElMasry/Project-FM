using Domain.Entities.PersonContainer;
using Domain.Entities.PlayersContainer;
using Domain.Entities.PlayersContainer.Attack;
using Domain.Entities.PlayersContainer.Midfield;
using Infrastructure;

try
{
    var InMemoryDB = InMemoryPlayerDatabase.GetInstance();

    var ptest = new Midfielder(new RealPerson("Nour", "2001-02-16", "Egyptian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CAM");
    var ptest2 = new Attacker(new RealPerson("John Doe", "1988-07-13", "English", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "RW");

    InMemoryDB.SavePlayer(ptest);
    InMemoryDB.SavePlayer(ptest2);

    PrintList<Player>(InMemoryDB.Players);

    InMemoryDB.RemovePlayer(ptest2);


    Console.WriteLine("Players in database after removal: \n");
    PrintList<Player>(InMemoryDB.Players);


    var InMemoryDB2 = InMemoryPlayerDatabase.GetInstance();

    Console.WriteLine("Players in second database instance: \n");
    PrintList<Player>(InMemoryDB2.Players);

    Console.ReadKey();

} catch (Exception e){
    Console.WriteLine(e);
}

static void PrintList<T>(List<T> list)
{
    for(int i = 0; i < list.Count; i++)
        Console.WriteLine($"{i + 1}:\n{list[i]}");
}