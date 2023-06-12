namespace Domain.Entities;
public static class FixtureGenerator
{
    private static readonly Random rnd = new();
    private static List<Fixture> fixtures;
    private static DateTime currentDate;
    public static async Task<List<Fixture>> Generate(League league)
    {
        fixtures = new List<Fixture>();
        var teamsFirstLegs = league.Teams;

        if (teamsFirstLegs.Count % 2 != 0)
            throw new ArgumentException("Can't have odd number of teams");

        currentDate = new DateTime(league.CurrentSeason.Year, 9, 1);
            
        await Task.Run(() => CreateLeg(league, teamsFirstLegs));

        await Task.Run(() => ReverseLegFixtures(league));

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

    private static Task ReverseLegFixtures(League league)
    {
        var AwayFixtures = new List<Fixture>();

        for(int i = 0; i < fixtures.Count; i++)
        {
            if(i % 10 == 0)
            {
                currentDate = currentDate.AddDays(7);
            }
            AwayFixtures.Add(new Fixture(fixtures[i].FixtureLeague, fixtures[i].AwayTeam, fixtures[i].HomeTeam) { Date = currentDate });
        }

        fixtures.AddRange(AwayFixtures);

        return Task.CompletedTask;
    }
}

