namespace Domain.Entities;
public class GoalkeepingStats : PlayerStats
{
    public GoalkeepingStats() { }

    public GoalkeepingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
    {
        OverallRating = goalkeeping;
    }
}
