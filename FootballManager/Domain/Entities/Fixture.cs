namespace Domain.Entities;
public class Fixture
{
    public long FixtureId { get; set; }
    public League League { get; set; }
    public string Venue { get; set; }
    public DateTime Date { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture(League league, string venue, DateTime date, Team homeTeam, Team awayTeam)
    {
        League = league;
        Venue = venue;
        Date = date;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;

        HomeTeam.AddFixture(this);
        AwayTeam.AddFixture(this);
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
        Console.WriteLine($"\nScore is: {HomeTeam.Name} {HomeTeamScore} - {AwayTeamScore} {AwayTeam.Name}");
    }
}

