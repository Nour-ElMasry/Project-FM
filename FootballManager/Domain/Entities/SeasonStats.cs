namespace Domain.Entities;
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
    public void AddHomeGame()
    {
        HomeGamesPlayed += 1;
        GamesPlayed += 1;
    }
    public void AddAwayGame()
    {
        AwayGamesPlayed += 1;
        GamesPlayed += 1;
    }

    public void AddGoalsFor(int gf) => GoalsFor += gf;
    public void AddGoalsAgainst(int ga) => GoalsAgainst += ga;

    public override string ToString()
    {
        return $"\n             Points: {Points}, Games Played: {GamesPlayed}pts, Home Games: {HomeGamesPlayed}, Away Games: {AwayGamesPlayed}, Goals For: {GoalsFor}, Goals Against: {GoalsAgainst}";
    }
}
