using System.Text;

namespace Domain.Entities;
public static class FixtureGenerator
{
    private static readonly Random rnd = new();
    private static List<Fixture> fixtures;
    private static DateTime currentDate = DateTime.Now;
    public static async Task<List<Fixture>> Generate(League league)
    {
        fixtures = new List<Fixture>();
        var teamsFirstLegs = league.Teams;

        await Task.Run(() => CreateLeg(league, teamsFirstLegs));

        await Task.Run(() => ReverseLegFixtures());

        return fixtures;
    }

    private static Task CreateLeg(League league, List<Team> legTeams)
    {
        for (var i = 0; i < legTeams.Count - 1; i++)
        {
            var firstHalf = 0;
            var secondHalf = legTeams.Count - 1;
            var halfTeams = (legTeams.Count - 1) / 2;

            while (firstHalf <= halfTeams && secondHalf > halfTeams)
            {
                var homeTeam = legTeams[firstHalf++];
                var awayTeam = legTeams[secondHalf--];
                fixtures.Add(new Fixture(league, homeTeam, awayTeam) { Date = currentDate });
            }

            var tmpTeamSwap = legTeams[legTeams.Count - 1];
            for (var j = legTeams.Count - 1; j > 1; j--)
            {
                legTeams[j] = legTeams[j - 1];
            }
            legTeams[1] = tmpTeamSwap;
            currentDate = currentDate.AddDays(7);
        }

        return Task.CompletedTask;
    }

    private static Task ReverseLegFixtures()
    {
        var AwayFixtures = new List<Fixture>();

        for(int i = 1; i < fixtures.Count + 1; i++)
        {
            if(i % 10 == 0)
            {
                currentDate = currentDate.AddDays(7);
            }
            AwayFixtures.Add(new Fixture(fixtures[i - 1].FixtureLeague, fixtures[i - 1].AwayTeam, fixtures[i - 1].HomeTeam) { Date = currentDate });
        }

        fixtures.AddRange(AwayFixtures);

        return Task.CompletedTask;
    }
}

