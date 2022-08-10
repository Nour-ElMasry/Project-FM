namespace Domain.Entities.SeasonContainer;
public class SeasonStats
{
    public int Points { get; set; }
    public int GamesPlayed { get; set; }
    public int HomeGamesPlayed { get; set; }
    public int AwayGamesPlayed { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }

    public SeasonStats(int points, int gamesPlayed, int homeGamesPlayed, int awayGamesPlayed, int goalsFor, int goalsAgainst)
    {
        Points = points;
        GamesPlayed = gamesPlayed;
        HomeGamesPlayed = homeGamesPlayed;
        AwayGamesPlayed = awayGamesPlayed;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
    }

    public SeasonStats()
    {
        Points = 0;
        GamesPlayed = 0;
        HomeGamesPlayed = 0;
        AwayGamesPlayed = 0;
        GoalsFor = 0;
        GoalsAgainst = 0;
    }

    public void AddPoints(int pts) => Points += pts;
    public void AddGamesPlayed(int gms) => GamesPlayed += gms;
    public void AddHomeGamesPlayed(int hgp) => HomeGamesPlayed += hgp;
    public void AddAwayGamesPlayed(int agp) => AwayGamesPlayed += agp;

    public void AddGoalsFor(int gf) => GoalsFor += gf;
    public void AddGoalsAgainst(int ga) => GoalsAgainst += ga;

}
