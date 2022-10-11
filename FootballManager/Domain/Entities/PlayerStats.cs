namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
public abstract class PlayerStats
{
    [Key]
    public long PlayerStatsId { get; set; }
    public int Attacking { get; set; }
    public int PlayMaking { get; set; }
    public int Defending { get; set; }
    public int Goalkeeping { get; set; }
    public int OverallRating { get; set; }

    public PlayerStats() { }
    public PlayerStats(int attacking, int playMaking, int defending, int goalkeeping)
    {
        if (attacking >= 100 || playMaking >= 100 || defending >= 100 || goalkeeping >= 100)
            throw new InvalidDataException("A player stat can't be larger than 99!");
        Attacking = attacking;
        PlayMaking = playMaking;
        Defending = defending;
        Goalkeeping = goalkeeping;
    }
}
