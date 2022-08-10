namespace Domain.Entities.PlayersContainer.Attack
{
    public class AttackingStats : PlayerStats
    {
        public AttackingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
        {
        }
    }
}
