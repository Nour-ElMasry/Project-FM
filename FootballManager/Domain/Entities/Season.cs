namespace Domain.Entities;
public class Season
{
    public int Year { get; set; }
    public League LeagueSeason { get; set; }
    public Season(int year, League league)
    {
        Year = year;
        LeagueSeason = league;
    }
}

