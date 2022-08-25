namespace Domain.Entities;
public abstract class Tactic
{
    public long TacticId { get; set; }
    public int AttackingWeight { get; set; }
    public int DefendingWeight { get; set; }
}
