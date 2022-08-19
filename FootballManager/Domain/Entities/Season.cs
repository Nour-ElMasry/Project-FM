namespace Domain.Entities;
public class Season
{
    public long SeasonId { get; set; }
    public int Year { get; set; }
    public long LeagueId { get; set; }
    public League LeagueSeason { get; set; }
    public Season(int year, League league)
    {
        Year = year;
        LeagueSeason = league;
    }
}

