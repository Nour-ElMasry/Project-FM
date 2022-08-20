namespace Domain.Entities;
public class SeasonStats
{
    public int Points { get; set; } = 0;
    public int GamesPlayed { get; set; } = 0;
    public int HomeGamesPlayed { get; set; } = 0;
    public int AwayGamesPlayed { get; set; } = 0;
    public int GoalsFor { get; set; } = 0;
    public int GoalsAgainst { get; set; } = 0;

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
        return $"\n             Points: {Points}pts, Games Played: {GamesPlayed}, Home Games: {HomeGamesPlayed}, Away Games: {AwayGamesPlayed}, Goals For: {GoalsFor}, Goals Against: {GoalsAgainst}";
    }
}
