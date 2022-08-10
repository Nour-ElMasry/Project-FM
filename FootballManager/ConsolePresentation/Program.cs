using Domain.Entities.PersonContainer;
using Domain.Entities.PlayersContainer.Midfield;

try
{
    var ptest = new Midfielder(new RealPerson("Nour", "2001-02-16", "Egyptian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CAM");
    Console.WriteLine(ptest.PlayerStats.ToString());
} catch (Exception e){
    Console.WriteLine(e);
}