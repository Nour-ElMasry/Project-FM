using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class League
{
    [Key]
    public long LeagueId { get; set; }
    public string Name { get; set; }

    public List<Team> Teams { get; set; } = new ();

    [ForeignKey("CurrentSeasonId")]
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
        foreach (var team in Teams) team.ResetSeason();
        CurrentSeason = new Season(++CurrentSeason.Year);
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

