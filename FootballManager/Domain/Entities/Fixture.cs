using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Fixture
{
    [Key]
    public long FixtureId { get; set; }

    [ForeignKey("FixtureLeagueID")]
    public League FixtureLeague { get; set; }

    [ForeignKey("HomeTeamID")]
    public Team HomeTeam { get; set; }

    [ForeignKey("AwayTeamID")]
    public Team AwayTeam { get; set; }

    public string Venue { get; set; } = "";
    public DateTime? Date { get; set; } = null;
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture() { }

    public Fixture(League leagueFixture, Team homeTeam, Team awayTeam)
    {
        FixtureLeague = leagueFixture;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;

        Venue = HomeTeam.Venue;
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
    } 
}

