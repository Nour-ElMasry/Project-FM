namespace Domain.Entities;
public class Fixture
{
    public long FixtureId { get; set; }
    public League LeagueFixture { get; set; }
    public List<Team> Teams { get; set; } = new();
    public string Venue { get; set; } = "";
    public DateTime? Date { get; set; } = null;
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture() { }

    public Fixture(League leagueFixture, Team homeTeam, Team awayTeam)
    {
        LeagueFixture = leagueFixture;
        Teams.Add(homeTeam);
        Teams.Add(awayTeam);

        Venue = Teams[0].Venue;
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
        Console.WriteLine($"\nScore is: {Teams[0].Name} {HomeTeamScore} - {AwayTeamScore} {Teams[1].Name}");
        PlayerStatsAddition();
    }

    private void PlayerStatsAddition()
    {
        var HomeTeamPlayers = Teams[0].Players;
        var AwayTeamPlayers = Teams[1].Players;

        HomeTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());
        AwayTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());

        if (AwayTeamScore == 0)
        {
            var CleanSheetPlayers = HomeTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }
        
        if (HomeTeamScore == 0)
        {
            var CleanSheetPlayers = AwayTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }
    }
}

