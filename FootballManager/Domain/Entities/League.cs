using Domain.Exceptions;

namespace Domain.Entities;
public class League
{
    public long LeagueId { get; set; }
    public string Name { get; set; }

    public List<Team> Teams { get; set; } = new ();

    public Season CurrentSeason { get; set; }

    public List<Season> SeasonsHistory { get; set; } = new ();
         
    public List<Fixture> Fixtures { get; set; } = new ();

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
        t.Leagues.Add(this);
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

