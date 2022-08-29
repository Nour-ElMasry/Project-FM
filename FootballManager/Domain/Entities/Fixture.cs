using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Fixture
{
    [Key]
    public long FixtureId { get; set; }

    [ForeignKey("FixtureLeagueID")]
    public League FixtureLeague { get; set; }

    public List<Team> Teams { get; set; } = new();
    public string Venue { get; set; } = "";
    public DateTime? Date { get; set; } = null;
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture() { }

    public Fixture(League leagueFixture, Team homeTeam, Team awayTeam)
    {
        FixtureLeague = leagueFixture;
        Teams.Add(homeTeam);
        Teams.Add(awayTeam);

        Venue = Teams[0].Venue;
    }

    public void SimulateFixture()
    {
        FixtureSimulation.Simulate(this);
    } 
}

