namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class League
{
    [Key]
    public long LeagueId { get; set; }
    public string Name { get; set; }
    public string LeagueLogo { get; set; } = "https://brandlogos.net/wp-content/uploads/2013/06/world-cup-vector-logo-400x400.png";

    [ForeignKey("CurrentSeasonId")]
    public Season CurrentSeason { get; set; }
    public List<Team> Teams { get; set; } = new();
    public List<Fixture> Fixtures { get; set; } = new();

    public League() { }
    public League(string name)
    {
        Name = name;
        CurrentSeason = new Season(DateTime.Now.Year);
    }
    public League(string name, string leagueLogo)
    {
        Name = name;
        CurrentSeason = new Season(DateTime.Now.Year);
        LeagueLogo = leagueLogo;
    }

    public void NextSeason()
    {
        foreach (var team in Teams) team.ResetSeason();
        CurrentSeason = new Season(CurrentSeason.Year + 1);
        Fixtures = new();
        this.CreateFixtures();
    }

    public void ResetLeague()
    {
        foreach (var team in Teams) team.ResetSeason();
        CurrentSeason = new Season(DateTime.Now.Year);
        Fixtures = new();
        this.CreateFixtures();
    }
    public void RemoveTeam(Team t)
    {
        var teamToRemove = Teams.First(te => t == te);
        if (teamToRemove == null)
            throw new NullReferenceException("Team doesn't exist in this League!");
        Teams.Remove(teamToRemove);
    }
    public void CreateFixtures()
    {
        Fixtures = FixtureGenerator.Generate(this).Result;
    }
}

