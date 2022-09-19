namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Fixture
{
    [Key]
    public long FixtureId { get; set; }

    [ForeignKey("FixtureLeagueId")]
    public League FixtureLeague { get; set; }

    [ForeignKey("HomeTeamId")]
    public Team HomeTeam { get; set; }

    [ForeignKey("AwayTeamId")]
    public Team AwayTeam { get; set; }

    [ForeignKey("FixtureScoreId")]
    public Score FixtureScore { get; set; }

    public string Venue { get; set; }
    public DateTime? Date { get; set; }
    public List<Event> FixtureEvents { get; set; } = new();
    public bool isPlayed { get; set; } = false;

    public Fixture() { }

    public Fixture(League leagueFixture, Team homeTeam, Team awayTeam)
    {
        FixtureLeague = leagueFixture;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;

        Venue = HomeTeam.Venue;
        FixtureScore = new Score
        {
            HomeScore = 0,
            AwayScore = 0
        };
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
    }
}

