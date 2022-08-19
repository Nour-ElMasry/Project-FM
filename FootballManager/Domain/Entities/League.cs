using Domain.Exceptions;

namespace Domain.Entities;
public class League
{
    public long LeagueId { get; set; }
    public string Name { get; set; }

    public ICollection<Team> Teams { get; set; } = new List<Team>();

    public Season CurrentSeason { get; set; }

    public ICollection<Season> SeasonsHistory { get; set; } = new List<Season>();
         
    public ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();

    public League(string name)
    {
        Name = name;
        CurrentSeason = new Season(DateTime.Now.Year, this);
    }

    public void AddSeasonHistory(Season s) => SeasonsHistory.Add(s);
    public void NextSeason()
    {
        AddSeasonHistory(CurrentSeason);
        foreach (var team in Teams) team.ResetSeasonStats();
        CurrentSeason = new Season(++CurrentSeason.Year, this);
    }

    public void AddTeam(Team t)
    {
        if (Teams.Contains(t))
            throw new AlreadyExistsException("Team already exists in this league!");
        Teams.Add(t);
        t.CurrentLeague = this;
    }

    public void RemoveTeam(Team t)
    {
        var teamToRemove = Teams.First(te => t == te);
        if (teamToRemove == null)
            throw new NullReferenceException("Team doesn't exist in this League!");
        Teams.Remove(teamToRemove);
    }

    public void CreateFixtures() {
        Fixtures = FixtureGenerator.Generate(this);
    }
}

