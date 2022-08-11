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

    InMemoryDB.WriteToFile();

    InMemoryDB.ReadPlayersFile();

    Console.ReadKey();

} catch (Exception e){
    Console.WriteLine(e);
}

