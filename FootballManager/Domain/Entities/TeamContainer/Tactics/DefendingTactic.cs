namespace Domain.Entities.TeamContainer.Tactics;
public class DefendingTactic : Tactic
{
    public DefendingTactic()
    {
        AttackingWeight = -10;
        DefendingWeight = 10;
    }
}

