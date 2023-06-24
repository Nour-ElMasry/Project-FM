namespace Domain.Entities;
public class AttackingTactic : Tactic
{
    public AttackingTactic()
    {
        AttackingWeight = 10;
        DefendingWeight = -5;
    }
}

