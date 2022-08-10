namespace Domain.Entities.PlayersContainer.Goalkeep;
public class GoalkeepingStats : PlayerStats
{
    public GoalkeepingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
    {
    }
}
