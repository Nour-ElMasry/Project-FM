using Domain.Entities;
using Infrastructure;

try
{
    var InMemoryDB = InMemoryPlayerDatabase.GetInstance();

    var ptest1team1 = new Midfielder(new Person("Kante", "1987-02-16", "France"), "CDM");
    var ptest2team1 = new Attacker(new Person("Havertz", "1988-07-13", "Germany"), "ST");
    var ptest3team1 = new Defender(new Person("Koulibaly", "1987-08-10", "Senegal"), "CB");
    var ptest4team1 = new Goalkeeper(new Person("Mendy", "1991-04-05", "Senegal"), "GK");

    var ptest1team2 = new Midfielder(new Person("Kevin De Bruyne", "1991-02-16", "Belgium"), "CAM");
    var ptest2team2 = new Attacker(new Person("Gabriel Jesus", "1995-07-13", "Brazil"), "ST");
    var ptest3team2 = new Defender(new Person("Ruben Dias", "1996-08-10", "Portugal"), "CB");
    var ptest4team2 = new Goalkeeper(new Person("Ederson", "1992-07-13", "Brazil"), "GK");

    var ptest1team3 = new Midfielder(new Person("Thiago", "1988-02-16", "Spain"), "CAM");
    var ptest2team3 = new Attacker(new Person("Mohamed Salah", "1994-07-13", "Egypt"), "RW");
    var ptest3team3 = new Defender(new Person("Van Dijk", "1994-08-10", "Netherlands"), "CB");
    var ptest4team3 = new Goalkeeper(new Person("Alisson", "1996-07-13", "Brazil"), "GK");

    var ptest1team4 = new Midfielder(new Person("Casemiro", "1988-02-16", "brazil"), "CDM");
    var ptest2team4 = new Attacker(new Person("Ronaldo", "1994-07-13", "Portugal"), "ST");
    var ptest3team4 = new Defender(new Person("Varane", "1994-08-10", "France"), "CB");
    var ptest4team4 = new Goalkeeper(new Person("De Gea", "1996-07-13", "Spain"), "GK");

    var teamTest = new Team("Chelsea", "England", "Stamford Bridge", new FakeManager(new Person("Tuchel", "2001-02-18", "Egypt")));
    var teamTest2 = new Team("Manchester City", "England", "Etihad stadium", new FakeManager(new Person("Pep Guardiola", "1977-02-18", "Spain")));
    var teamTest3 = new Team("Liverpool", "England", "Anfield stadium", new FakeManager(new Person("Klopp", "1975-02-18", "Germany")));
    var teamTest4 = new Team("Manchester Utd", "England", "Old Trafford stadium", new FakeManager(new Person("Ten Hag", "1975-02-18", "Netherlands")));


    teamTest.CurrentTeamSheet.TeamTactic = new DefendingTactic();
    teamTest.AddPlayer(ptest1team1);
    teamTest.AddPlayer(ptest2team1);
    teamTest.AddPlayer(ptest3team1);
    teamTest.AddPlayer(ptest4team1);


    teamTest2.CurrentTeamSheet.TeamTactic = new AttackingTactic();
    teamTest2.AddPlayer(ptest1team2);
    teamTest2.AddPlayer(ptest2team2);
    teamTest2.AddPlayer(ptest3team2);
    teamTest2.AddPlayer(ptest4team2);

    teamTest3.AddPlayer(ptest1team3);
    teamTest3.AddPlayer(ptest2team3);
    teamTest3.AddPlayer(ptest3team3);
    teamTest3.AddPlayer(ptest4team3);

    teamTest4.AddPlayer(ptest1team4);
    teamTest4.AddPlayer(ptest2team4);
    teamTest4.AddPlayer(ptest3team4);
    teamTest4.AddPlayer(ptest4team4);

    var testLeague = new League("English Premier League");

    testLeague.AddTeam(teamTest);
    testLeague.AddTeam(teamTest2);
    testLeague.AddTeam(teamTest3);
    testLeague.AddTeam(teamTest4);

    testLeague.CreateFixtures();

    Console.WriteLine($"{teamTest.CurrentLeague.Name} Fixture: ");

    for (int i = 0; i < testLeague.Fixtures.Count; i++)
    {
        Console.WriteLine($"\nGame {i + 1}:");
        testLeague.Fixtures.ToList()[i].SimulateFixture();
    }

    Console.WriteLine("\n----------------------------------------------------------");

    var standings = testLeague.Teams.OrderByDescending(t => t.CurrentSeasonStats.Points).ThenByDescending(t => t.CurrentSeasonStats.GoalsFor - t.CurrentSeasonStats.GoalsAgainst).ToList();

    Console.WriteLine($"\n{testLeague.Name}'s league standings:\n");

    for(int i = 0; i < standings.Count; i++)
    {
        var team = standings[i];
        Console.WriteLine($"{i + 1} - {team.Name} -> {team.CurrentSeasonStats}");
    }

    Console.ReadKey();

} catch (Exception e){
    Console.WriteLine(e);
}

