namespace Domain.Entities;
public class DefendingTactic : Tactic
{
    public DefendingTactic()
    {
        AttackingWeight = -5;
        DefendingWeight = 20;
    }
}

