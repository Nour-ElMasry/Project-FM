namespace Domain.Entities
{
    public class AttackingStats : PlayerStats
    {
        public AttackingStats() { }
        public AttackingStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
        {
            OverallRating = (int)Math.Ceiling((attacking + playMaking) / 2.0);
        }
    }
}
