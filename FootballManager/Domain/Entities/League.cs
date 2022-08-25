using Domain.Exceptions;

namespace Domain.Entities;
public class League
{
    public long LeagueId { get; set; }
    public string Name { get; set; }

    public List<Team> Teams { get; set; } = new ();

    public long CurrentSeasonId { get; set; }
    public Season CurrentSeason { get; set; }
         
    public List<Fixture> Fixtures { get; set; } = new ();

    public League() { }
    public League(string name)
    {
        Name = name;
        CurrentSeason = new Season(DateTime.Now.Year);
    }
    public void NextSeason()
    {
        foreach (var team in Teams) team.ResetSeasonStats();
        CurrentSeason = new Season(++CurrentSeason.Year);
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

