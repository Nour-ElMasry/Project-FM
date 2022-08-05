namespace Domain.Entities;
public class Season
{
    private static int _id = 0;
    public int Id { get; set; }
    public int Year { get; set; }

    public IReadOnlyList<Team> SeasonStandings{ get; set; }

    public Season(int year, IReadOnlyList<Team> seasonStandings)
    {
        Id = ++_id;
        Year = year;
        SeasonStandings = seasonStandings;
    }   
}

