namespace Domain.Entities;
public class DefendingStats : PlayerStats
{
    public DefendingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
    {
    }
}