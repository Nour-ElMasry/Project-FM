using Domain.Entities;

try
{

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

    var ptest1team5 = new Midfielder(new Person("Holjberg", "1988-02-16", "Norway"), "CDM");
    var ptest2team5 = new Attacker(new Person("Son", "1994-07-13", "South Korea"), "CF");
    var ptest3team5 = new Defender(new Person("Sanchez", "1994-08-10", "Columbia"), "CB");
    var ptest4team5 = new Goalkeeper(new Person("Lloris", "1996-07-13", "France"), "GK");

    var ptest1team6 = new Midfielder(new Person("Odegarrd", "1988-02-16", "Norway"), "CAM");
    var ptest2team6 = new Attacker(new Person("Gabriel Jesus", "1994-07-13", "Brazil"), "ST");
    var ptest3team6 = new Defender(new Person("Ben White", "1994-08-10", "England"), "CB");
    var ptest4team6 = new Goalkeeper(new Person("Ramsdale", "1996-07-13", "England"), "GK");

    var teamTest = new Team("Chelsea", "England", "Stamford Bridge", new FakeManager(new Person("Tuchel", "2001-02-18", "Egypt")));
    var teamTest2 = new Team("Manchester City", "England", "Etihad stadium", new FakeManager(new Person("Pep Guardiola", "1977-02-18", "Spain")));
    var teamTest3 = new Team("Liverpool", "England", "Anfield stadium", new FakeManager(new Person("Klopp", "1975-02-18", "Germany")));
    var teamTest4 = new Team("Manchester Utd", "England", "Old Trafford stadium", new FakeManager(new Person("Ten Hag", "1975-02-18", "Netherlands")));
    var teamTest5= new Team("Tottenham", "England", "Tottenham Hotspur Stadium", new FakeManager(new Person("Conte", "1975-02-18", "Italy")));
    var teamTest6 = new Team("Arsenal", "England", "Emirates Stadium", new FakeManager(new Person("Arteta", "1975-02-18", "Spain")));


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

    teamTest5.CurrentTeamSheet.TeamTactic = new DefendingTactic();
    teamTest5.AddPlayer(ptest1team5);
    teamTest5.AddPlayer(ptest2team5);
    teamTest5.AddPlayer(ptest3team5);
    teamTest5.AddPlayer(ptest4team5);

    teamTest6.CurrentTeamSheet.TeamTactic = new AttackingTactic();
    teamTest6.AddPlayer(ptest1team6);
    teamTest6.AddPlayer(ptest2team6);
    teamTest6.AddPlayer(ptest3team6);
    teamTest6.AddPlayer(ptest4team6);

    var testLeague = new League("English Premier League");

    testLeague.AddTeam(teamTest);
    testLeague.AddTeam(teamTest2);
    testLeague.AddTeam(teamTest3);
    testLeague.AddTeam(teamTest4);
    testLeague.AddTeam(teamTest5);
    testLeague.AddTeam(teamTest6);

    testLeague.CreateFixtures();

    Console.WriteLine($"{testLeague.Name} Fixture: ");

    for (int i = 0; i < testLeague.Fixtures.Count; i++)
    {
        Console.WriteLine($"\nGame {i + 1}:");
        testLeague.Fixtures[i].SimulateFixture();
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

