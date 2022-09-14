using Domain.Entities;
using Infrastructure;

namespace IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static async Task InitializeDbForTests(DataContext db)
        {
            var league = new League("Fake League") { LeagueId = 1 };

            var team = new Team("Fake Team1", "Country9", "Fake Stadium") { TeamId = 1, CurrentLeague = league };

            var player1 = new Attacker(new Person("RightWinger Player", "2001-02-16", "Country1")) { PlayerId = 1, CurrentTeam = team };
            var player2 = new Midfielder(new Person("CenterMid Player", "2001-02-16", "Country2")) { PlayerId = 2, CurrentTeam = team };
            var player3 = new Defender(new Person("RightBack Player", "2001-02-16", "Country3")) { PlayerId = 3, CurrentTeam = team };
            var player4 = new Goalkeeper(new Person("Goalkeeper Player", "2001-02-16", "Country4")) { PlayerId = 4, CurrentTeam = team };

            team.Players.Add(player1);
            team.Players.Add(player2);
            team.Players.Add(player3);
            team.Players.Add(player4);

            var team1 = new Team("Fake Team2", "Country10", "Fake Stadium") { TeamId = 2, CurrentLeague = league };

            var player5 = new Attacker(new Person("RightWinger Player", "2001-02-16", "Country5")) { PlayerId = 5, CurrentTeam = team1 };
            var player6 = new Midfielder(new Person("CenterMid Player", "2001-02-16", "Country6")) { PlayerId = 6, CurrentTeam = team1 };
            var player7 = new Defender(new Person("RightBack Player", "2001-02-16", "Country7")) { PlayerId = 7, CurrentTeam = team1 };
            var player8 = new Goalkeeper(new Person("Goalkeeper Player", "2001-02-16", "Country8")) { PlayerId = 8, CurrentTeam = team1 };

            team1.Players.Add(player5);
            team1.Players.Add(player6);
            team1.Players.Add(player7);
            team1.Players.Add(player8);

            var teamManager = new FakeManager(new Person("FakeManager1", "2001-02-16", "Country11")) { ManagerId = 1 };

            var user = new User("username", "password", new Person("UserPerson", "2001-02-16", "UserCountry"));

            var team1Manager = new RealManager(user) { ManagerId = 2 };

            team.TeamManager = teamManager;
            team1.TeamManager = team1Manager;

            league.Teams.Add(team);
            league.Teams.Add(team1);

            league.CreateFixtures();

            league.Fixtures.ForEach(f => f.FixtureLeague = league);

            await db.Leagues.AddAsync(league);
            await db.SaveChangesAsync();
        }
    }
}
