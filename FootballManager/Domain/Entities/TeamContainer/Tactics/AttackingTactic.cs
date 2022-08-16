namespace Domain.Entities.TeamContainer.Tactics;
public class AttackingTactic : Tactic
{
    public AttackingTactic()
    {
        AttackingWeight = 10;
        DefendingWeight = -10;
    }
}

