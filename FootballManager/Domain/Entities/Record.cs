using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Record
{
    [Key]
    public long RecordId { get; set; }
    public int GamesPlayed { get; set; } = 0;
    public int Goals { get; set; } = 0;
    public int Assists { get; set; } = 0;
    public int CleanSheets { get; set; } = 0;

    public void AddGamePlayed() => GamesPlayed++;
    public void AddGoal() => Goals++;
    public void AddAssist() => Assists++;
    public void AddCleanSheet() => CleanSheets++;
}
