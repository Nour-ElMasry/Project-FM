namespace Domain.Entities
{
    public class AttackingStats : PlayerStats
    {
        public AttackingStats() { }
        public AttackingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
        {
        }
    }
}
