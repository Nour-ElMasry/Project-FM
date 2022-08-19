using Domain.Entities;
using Infrastructure;

try
{
    var InMemoryDB = InMemoryPlayerDatabase.GetInstance();

    var ptest1team1 = new Midfielder(new FakePerson("Modric", "1987-02-16", "Croatian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CM");
    var ptest2team1 = new Attacker(new FakePerson("Benzema", "1988-07-13", "French", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "ST");
    var ptest3team1 = new Defender(new FakePerson("Sergio Ramos", "1987-08-10", "Spanish", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CB");
    var ptest4team1 = new Goalkeeper(new FakePerson("Courtois", "1991-04-05", "Belgium", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "GK");

    var ptest1team2 = new Midfielder(new FakePerson("Kevin De Bruyne", "1991-02-16", "Belgium", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CAM");
    var ptest2team2 = new Attacker(new FakePerson("Gabriel Jesus", "1995-07-13", "Brazilian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "ST");
    var ptest3team2 = new Defender(new FakePerson("Ruben Dias", "1996-08-10", "Portoguese", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CB");
    var ptest4team2 = new Goalkeeper(new FakePerson("Ederson", "1992-07-13", "Brazilian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "GK");

    var ptest1team3 = new Midfielder(new FakePerson("Thiago", "1988-02-16", "Spanish", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CAM");
    var ptest2team3 = new Attacker(new FakePerson("Mohamed Salah", "1994-07-13", "Egyptian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "RW");
    var ptest3team3 = new Defender(new FakePerson("Van Dijk", "1994-08-10", "Deutch", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "CB");
    var ptest4team3 = new Goalkeeper(new FakePerson("Alisson", "1996-07-13", "Brazilian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"), "GK");

    var teamTest = new Team("Real Madrid", "Spain", "Santiago Bernabeu", new RealManager(new User("Slim01Fifi", "A76s6d7satdjj12@", new RealPerson("Nour Eldin", "2001-02-18", "Egyptian", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png"))));
    var teamTest2 = new Team("Manchester City", "England", "Etihad stadium", new FakeManager(new FakePerson("Pep Guardiola", "1977-02-18", "Spanish", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png")));
    var teamTest3 = new Team("Liverpool", "England", "Anfield stadium", new FakeManager(new FakePerson("Klopp", "1975-02-18", "German", "D://Stuff//Work-stuff//Project//Project-FM//FootballManagerDomainModel.png")));

    teamTest.AddPlayer(ptest1team1);
    teamTest.AddPlayer(ptest2team1);
    teamTest.AddPlayer(ptest3team1);
    teamTest.AddPlayer(ptest4team1);

    teamTest2.AddPlayer(ptest1team2);
    teamTest2.AddPlayer(ptest2team2);
    teamTest2.AddPlayer(ptest3team2);
    teamTest2.AddPlayer(ptest4team2);

    teamTest3.AddPlayer(ptest1team3);
    teamTest3.AddPlayer(ptest2team3);
    teamTest3.AddPlayer(ptest3team3);
    teamTest3.AddPlayer(ptest4team3);

    var testLeague = new League("UEFA Champions League");

    testLeague.AddTeam(teamTest);
    testLeague.AddTeam(teamTest2);
    testLeague.AddTeam(teamTest3);

    var testFixture = new Fixture(testLeague, teamTest.Venue, new DateTime(2022,5,4), teamTest, teamTest2);

    var testFixture2 = new Fixture(testLeague, teamTest.Venue, new DateTime(2022, 5, 28), teamTest, teamTest3);

    Console.WriteLine($"{teamTest.CurrentLeague.Name} Fixture: ");

    testFixture.SimulateFixture();

    testFixture2.SimulateFixture();

    Console.WriteLine("\n----------------------------------------------------------");

    var standings = testLeague.Teams.OrderByDescending(t => t.CurrentSeasonStats.Points).ToList();

    Console.WriteLine($"\n{testLeague.Name}'s league standings:\n");
    standings.ForEach(t => Console.WriteLine($"{t.Name} -> {t.CurrentSeasonStats}"));

    Console.ReadKey();

} catch (Exception e){
    Console.WriteLine(e);
}

