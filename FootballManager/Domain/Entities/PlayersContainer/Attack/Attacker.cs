using Domain.Exceptions;

namespace Domain.Entities.PlayersContainer.Attack;
public class Attacker : Player
{
    public Attacker(Person p, string position) : base(p, position)
    {
    }

    public Attacker(Person p, string position, AttackingStats aps) : base(p, position, aps)
    {
    }
}


