namespace Domain.Entities;
public class DefendingTactic : Tactic
{
    public DefendingTactic()
    {
        AttackingWeight = -10;
        DefendingWeight = 10;
    }
}

