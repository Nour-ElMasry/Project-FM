using Domain.Entities.FixtureContainer;
using Domain.Entities.LeagueContainer;
using Domain.Entities.ManagerContainer;
using Domain.Entities.PlayersContainer;
using Domain.Entities.SeasonContainer;
using Domain.Exceptions;

namespace Domain.Entities.TeamContainer;
public class Team
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }
    public Manager TeamManager { get; set; }
    public League? CurrentLeague { get; set; }
    public SeasonStats CurrentSeasonStats { get; set; }
    private List<SeasonStats> SeasonStatsHistory { get; set; }
    private List<Player> Players { get; set; }
    private List<Fixture> Fixtures { get; set; }


    public Team(string name, string country, string venue, Manager manager)
    {
        Name = name;
        Country = country;
        Venue = venue;
        TeamManager = manager;

        CurrentSeasonStats = new();
        SeasonStatsHistory = new();

        Players = new List<Player>();
        Fixtures = new List<Fixture>();
    }

    public void AddPlayer(Player p)
    {
        if (Players.Contains(p))
            throw new AlreadyExistsException("Player already exists in this team!");
        Players.Add(p);
        p.CurrentTeam = this;
    }

    public void RemovePlayer(Player p)
    {
        var playerToRemove = Players.Find(pl => p == pl);
        if (playerToRemove == null)
            throw new NullReferenceException("Player doesn't exist in this team!");
        Players.Remove(playerToRemove);
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
