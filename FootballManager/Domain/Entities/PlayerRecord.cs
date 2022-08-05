namespace Domain.Entities;
public class PlayerRecord
{
    public int GoalsScored { get; set; }
    public int GoalsAssisted { get; set; }
    public int Saves { get; set; }
    public int CleanSheets { get; set; }

    public PlayerRecord(int goalsScored, int goalsAssisted, int saves, int cleanSheets)
    {
        GoalsScored = goalsScored;
        GoalsAssisted = goalsAssisted;
        Saves = saves;
        CleanSheets = cleanSheets;
    }

    public PlayerRecord()
    {
        GoalsScored = 0;
        GoalsAssisted = 0;
        Saves = 0;
        CleanSheets = 0;
    }

}

