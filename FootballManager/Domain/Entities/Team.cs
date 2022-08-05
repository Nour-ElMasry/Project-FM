
using Domain.Exceptions;

namespace Domain.Entities;
public class Team
{
    private static int _id = 0;
    private readonly List<Player> _players = new();
    private readonly List<Fixture> _fixtures = new();
    public long Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }
    public IManager Manager { get; set; }
    public League? CurrentLeague { get; set; } 
    public SeasonStats CurrentSeasonStats { get; set; }

    public Team(string name, string country, string venue, IManager manager)
    {
        Id = ++_id;
        Name = name;
        Country = country;
        Venue = venue;
        Manager = manager;
        Manager.CurrentTeam = this;
        CurrentSeasonStats = new();
    }

    public IReadOnlyList<Player> GetPlayers() => _players;

    public void AddPlayer(Player p)
    {
        if (_players.Contains(p))
            throw new AlreadyExistsException("Player already exists in this team!");
        _players.Add(p);
        p.CurrentTeam = this;
    }

    public void RemovePlayer(long pId)
    {
        var playerToRemove = _players.Find(p => p.Id == pId);
        if(playerToRemove == null)
            throw new NullReferenceException("Player doesn't exist in this team!");
        _players.Remove(playerToRemove);
    }

    public void AddFixture(Fixture f) {
        if (_fixtures.Contains(f))
            throw new AlreadyExistsException("Player already exists in this team!");
        _fixtures.Add(f);
    }
}
