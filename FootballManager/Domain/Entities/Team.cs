using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Team
{
    [Key]
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }

    public Team() { }
    public Team(string name, string country, string venue)
    {
        Name = name;
        Country = country;
        Venue = venue;

        CurrentTeamSheet = new TeamSheet();
        CurrentSeasonStats = new SeasonStats();
    }

    [ForeignKey("TeamManagerId")]
    public Manager TeamManager { get; set; }

    [ForeignKey("CurrentSeasonStatsId")]
    public SeasonStats CurrentSeasonStats { get; set; }

    [ForeignKey("CurrentTeamSheetId")]
    public TeamSheet CurrentTeamSheet { get; set; }

    [ForeignKey("CurrentLeagueId")]
    public League CurrentLeague { get; set; }

    public List<Player> Players { get; set; } = new();
    public List<Fixture> HomeFixtures { get; set; } = new();
    public List<Fixture> AwayFixtures { get; set; } = new();

    public void ResetSeason() {
        CurrentSeasonStats = new SeasonStats();
        Players.ForEach(p => p.ResetPlayerRecord());
    }
}
