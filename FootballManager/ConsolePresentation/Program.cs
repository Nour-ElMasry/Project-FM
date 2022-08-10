using Domain.Entities;
using Domain.Entities.PlayersContainer.Attack;
using Domain.Entities.PlayersContainer.Goalkeep;
using Domain.Entities.PlayersContainer.Midfield;

try
{
    var ptest = new Midfielder(new Person("Nour", "2001-02-16", "Egyptian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CAM");
    Console.WriteLine(ptest.PlayerStats.ToString());
} catch (Exception e){
    Console.WriteLine(e);
}