namespace Domain.Entities;
public class Team
{
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }
    public long TeamManagerId { get; set; }
    public Manager TeamManager { get; set; }

    public Team() { }
    public Team(string name, string country, string venue, long teamManagerId)
    {
        Name = name;
        Country = country;
        Venue = venue;
        TeamManagerId = teamManagerId;
        CurrentTeamSheet = new TeamSheet();
    }

    public long CurrentSeasonStatsId { get; set; }
    public SeasonStats CurrentSeasonStats { get; set; } = new();

    public long CurrentTeamSheetId { get; set; }
    public TeamSheet CurrentTeamSheet { get; set; }

    public long? CurrentLeagueId { get; set; }
    public League? CurrentLeague { get; set; }

    public List<Player> Players { get; set; } = new();
    public List<Fixture> Fixtures { get; set; } = new();

    public void ResetSeasonStats()
    {
        CurrentSeasonStats = new();
    }
}
