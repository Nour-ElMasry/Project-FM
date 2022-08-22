namespace Domain.Entities;
public class Fixture
{
    public long FixtureId { get; set; }
    public League League { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public string Venue { get; set; }
    public DateTime Date { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture(League league, Team homeTeam, Team awayTeam)
    {
        League = league;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;

        Venue = HomeTeam.Venue;

        HomeTeam.AddFixture(this);
        AwayTeam.AddFixture(this);
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
        Console.WriteLine($"\nScore is: {HomeTeam.Name} {HomeTeamScore} - {AwayTeamScore} {AwayTeam.Name}");
        PlayerStatsAddition();
    }

    private void PlayerStatsAddition()
    {
        var HomeTeamPlayers = HomeTeam.Players;
        var AwayTeamPlayers = AwayTeam.Players;

        HomeTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());
        AwayTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());

        if (AwayTeamScore == 0)
        {
            var CleanSheetPlayers = HomeTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }
        else
        {
            var CleanSheetPlayers = AwayTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }
    }
}

