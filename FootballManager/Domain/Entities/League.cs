using Domain.Exceptions;

namespace Domain.Entities;
public class League
{
    private static int _id = 0;
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Team> Teams { get; set; }

    public Season CurrentSeason { get; set; }

    public List<Season> SeasonsHistory { get; set; }

    public List<Fixture> Fixtures { get; set; }

    public League(string name, List<Team> teams)
    {
        Id = ++_id;
        Name = name;
        Teams = teams;
        CurrentSeason = new Season(DateTime.Now.Year, Teams);
        SeasonsHistory = new();
        Fixtures = new(); //Will implement an algorithm to generate the fixtures
    }

    public void AddSeasonHistory(Season s) => SeasonsHistory.Add(s);
    public void NextSeason() => CurrentSeason = new Season(++CurrentSeason.Year, Teams);

    public void AddTeam(Team t)
    {
        if (Teams.Contains(t))
            throw new AlreadyExistsException("Team already exists in this league!");
        Teams.Add(t);
        t.CurrentLeague = this;
    }

    public void RemoveTeam(long tId) {
        var teamToRemove = Teams.Find(t => t.Id == tId);
        if (teamToRemove == null)
            throw new NullReferenceException("Team doesn't exist in this League!");
        Teams.Remove(teamToRemove);
    }
    
}

