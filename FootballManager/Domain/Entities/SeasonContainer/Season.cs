using Domain.Entities.LeagueContainer;
using Domain.Entities.TeamContainer;

namespace Domain.Entities.SeasonContainer;
public class Season
{
    public int Year { get; set; }
    public League LeagueSeason { get; set; }
    public List<Team> SeasonStandings { get; set; }

    public Season(int year, League league)
    {
        Year = year;
        LeagueSeason = league;
        SeasonStandings = LeagueSeason.Teams;
    }
}

