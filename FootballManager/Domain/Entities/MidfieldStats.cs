namespace Domain.Entities;
public class MidfieldStats : PlayerStats
{
    public MidfieldStats() { }
    public MidfieldStats(int attacking, int playMaking, int defending, int goalkeeping) : base(attacking, playMaking, defending, goalkeeping)
    {
        OverallRating = (int)Math.Ceiling((attacking + playMaking + defending) / 3.0);
    }
}

