namespace Domain.Entities;
public static class FixtureGenerator
{
    private static readonly Random rnd = new();
    private static List<Fixture> fixtures;
    public static List<Fixture> Generate(League league)
    {
        fixtures = new List<Fixture>();
        
        foreach (var t1 in league.Teams)
        {
            var team1 = t1;
            foreach (var t2 in league.Teams)
            {
                var team2 = t2;
                if(team1 == team2)
                    continue;
                fixtures.Add(new Fixture(league, team1, team2));
            }
        }

        ShuffleFixture();
        AssignDates();

        return fixtures;
    }

    private static void ShuffleFixture()
    {
        fixtures = fixtures.OrderBy(f => rnd.Next()).ToList();
    }

    private static void AssignDates()
    {
        var currentDate = DateTime.Now;

        foreach (var f in fixtures)
        {
            f.Date = currentDate;
            currentDate = currentDate.AddDays(7);
        }
    }
}

