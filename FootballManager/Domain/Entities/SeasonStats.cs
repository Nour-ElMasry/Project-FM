using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class SeasonStats
{
    [Key]
    public long SeasonStatsId { get; set; }
    public int Points { get; set; } = 0;
    public int GamesPlayed { get; set; } = 0;
    public int GamesWon { get; set; } = 0;
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; } = 0;
    public int HomeGamesPlayed { get; set; } = 0;
    public int AwayGamesPlayed { get; set; } = 0;
    public int GoalsFor { get; set; } = 0;
    public int GoalsAgainst { get; set; } = 0;
    public void AddWin()
    {
        GamesWon++;
        Points += 3;
    }
    public void AddDraw()
    {
        GamesDrawn++;
        Points += 1;
    }
    public void AddLose() => GamesLost++;
    public void AddHomeGame()
    {
        HomeGamesPlayed++;
        GamesPlayed++;
    }
    public void AddAwayGame()
    {
        AwayGamesPlayed++;
        GamesPlayed++;
    }

    public void AddGoalsFor(int gf) => GoalsFor += gf;
    public void AddGoalsAgainst(int ga) => GoalsAgainst += ga;

    public override string ToString()
    {
        return $"\n             Points: {Points}pts, Games Played: {GamesPlayed}, Wons: {GamesWon}, Draw: {GamesDrawn}, Loses: {GamesLost}, Home Games: {HomeGamesPlayed}, Away Games: {AwayGamesPlayed}, Goals For: {GoalsFor}, Goals Against: {GoalsAgainst}";
    }
}
