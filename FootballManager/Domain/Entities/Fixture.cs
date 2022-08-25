namespace Domain.Entities;
public class Fixture
{
    public long FixtureId { get; set; }
    public League LeagueFixture { get; set; }
    public List<Team> teams { get; set; } = new();
    public string Venue { get; set; } = "";
    public DateTime? Date { get; set; } = null;
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture() { }

    public Fixture(League leagueFixture, Team homeTeam, Team awayTeam)
    {
        LeagueFixture = leagueFixture;
        teams.Add(homeTeam);
        teams.Add(awayTeam);

        Venue = teams[0].Venue;
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
        Console.WriteLine($"\nScore is: {teams[0].Name} {HomeTeamScore} - {AwayTeamScore} {teams[1].Name}");
        PlayerStatsAddition();
    }

    private void PlayerStatsAddition()
    {
        var HomeTeamPlayers = teams[0].Players;
        var AwayTeamPlayers = teams[1].Players;

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

