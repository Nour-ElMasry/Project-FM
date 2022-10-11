namespace Domain.Entities;
public class DefendingStats : PlayerStats
{
    public DefendingStats() { }
    public DefendingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
    {
        OverallRating = (int)(Math.Ceiling((playMaking + defending) / 2.0));
    }
}