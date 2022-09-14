namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
public abstract class Tactic
{
    [Key]
    public long TacticId { get; set; }
    public int AttackingWeight { get; set; }
    public int DefendingWeight { get; set; }
}
