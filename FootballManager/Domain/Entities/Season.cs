namespace Domain.Entities;
public class Season
{
    public int Year { get; set; }

    public List<Team> SeasonStandings { get; set; }

    public Season(int year, List<Team> seasonStandings)
    {
        Year = year;
        SeasonStandings = seasonStandings;
    }
}

