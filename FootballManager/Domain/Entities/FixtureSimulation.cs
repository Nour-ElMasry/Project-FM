namespace Domain.Entities;
public static class FixtureSimulation
{
    private static readonly Random rnd = new();
    private static Fixture fixture;
    private static Team AttackingTeam;
    private static Team DefendingTeam;
    private static double AttackChance;
    public static void Simulate(Fixture f)
    {
        fixture = f;
        var numberOfEvents = rnd.Next(0, 10);


        for (int i = 0; i < numberOfEvents; i++)
        {
            TeamSelection(fixture.HomeTeam, fixture.AwayTeam);
            RandomChance(AttackChance);
        }

        AddGoalsAndGames();
        AddPointsToTeams();
    }

    private static void TeamSelection(Team homeTeam, Team AwayTeam)
    {
        var teamRnd = rnd.Next(1, 3);
        switch (teamRnd)
        {
            case 1:
                AttackingTeam = homeTeam;
                DefendingTeam = AwayTeam;
                break;
            case 2:
                AttackingTeam = AwayTeam;
                DefendingTeam = homeTeam;
                break;
            default:
                AttackingTeam = homeTeam;
                DefendingTeam = AwayTeam;
                break;
        }

        var total = (double)AttackingTeam.CurrentTeamSheet.AttackingRating + DefendingTeam.CurrentTeamSheet.DefendingRating;

        AttackChance = Math.Round(((double)AttackingTeam.CurrentTeamSheet.AttackingRating / total) * 100);
    }

    private static void RandomChance(double AttackingSuccessPercentage)
    {
        var range = 100000;

        var chance = rnd.Next(0, range);

        if (chance <= AttackingSuccessPercentage * range)
            if (fixture.HomeTeam == AttackingTeam)
                fixture.HomeTeamScore += 1;
            else
                fixture.AwayTeamScore += 1;
    }

    private static void AddPointsToTeams()
    { 
        if (fixture.HomeTeamScore > fixture.AwayTeamScore)
            fixture.HomeTeam.CurrentSeasonStats.AddPoints(3);
        else if (fixture.HomeTeamScore < fixture.AwayTeamScore)
            fixture.AwayTeam.CurrentSeasonStats.AddPoints(3);
        else
        {
            fixture.HomeTeam.CurrentSeasonStats.AddPoints(1);
            fixture.AwayTeam.CurrentSeasonStats.AddPoints(1);
        }
    }

    private static void AddGoalsAndGames()
    {
        fixture.HomeTeam.CurrentSeasonStats.AddGoalsFor(fixture.HomeTeamScore);
        fixture.HomeTeam.CurrentSeasonStats.AddGoalsAgainst(fixture.AwayTeamScore);

        fixture.AwayTeam.CurrentSeasonStats.AddGoalsFor(fixture.AwayTeamScore);
        fixture.AwayTeam.CurrentSeasonStats.AddGoalsAgainst(fixture.HomeTeamScore);

        fixture.HomeTeam.CurrentSeasonStats.AddHomeGame();
        fixture.AwayTeam.CurrentSeasonStats.AddAwayGame();
    }
}

