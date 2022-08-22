using Domain.Exceptions;

namespace Domain.Entities;
public class Team
{
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }
    public Manager TeamManager { get; set; }
    public List<League> Leagues { get; set; } = new ();
    public SeasonStats CurrentSeasonStats { get; set; } = new ();
    public List<SeasonStats> SeasonStatsHistory { get; set; } = new ();
    public List<Player> Players { get; set; } = new ();
    public List<Fixture> Fixtures { get; set; } = new ();
    public TeamSheet CurrentTeamSheet { get; set; } = new ();

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
