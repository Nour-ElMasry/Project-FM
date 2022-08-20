using Domain.Exceptions;

namespace Domain.Entities;
public class Team
{
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }
    public Manager TeamManager { get; set; }
    public League CurrentLeague { get; set; } = null;
    public SeasonStats CurrentSeasonStats { get; set; } = new();
    public ICollection<SeasonStats> SeasonStatsHistory { get; set; } = new List<SeasonStats>();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();
    public TeamSheet CurrentTeamSheet { get; set; } = new TeamSheet();

    public Team(string name, string country, string venue, Manager manager)
    {
        Name = name;
        Country = country;
        Venue = venue;
        TeamManager = manager;
        TeamManager.CurrentTeam = this;
    }

    public void AddPlayer(Player p)
    {
        if (Players.Contains(p))
            throw new AlreadyExistsException("Player already exists in this team!");
        Players.Add(p);
        p.CurrentTeam = this;

        CurrentTeamSheet.UpdateRating(Players);
    }

    public void RemovePlayer(Player p)
    {
        var playerToRemove = Players.First(pl => p == pl);
        if (playerToRemove == null)
            throw new NullReferenceException("Player doesn't exist in this team!");
        Players.Remove(playerToRemove);

        CurrentTeamSheet.UpdateRating(Players);
    }

    public void AddFixture(Fixture f)
    {
        if (Fixtures.Contains(f))
            throw new AlreadyExistsException("Player already exists in this team!");
        Fixtures.Add(f);
    }

    public void ResetSeasonStats()
    {
        SeasonStatsHistory.Add(CurrentSeasonStats);
        CurrentSeasonStats = new();
    }
}
