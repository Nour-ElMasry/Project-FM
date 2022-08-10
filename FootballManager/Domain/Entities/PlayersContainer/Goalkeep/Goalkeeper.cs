using Domain.Entities.PersonContainer;
using Domain.Exceptions;

namespace Domain.Entities.PlayersContainer.Goalkeep;
public class Goalkeeper : Player
{
    public Goalkeeper(Person p, string position) : base(p, position)
    {
    }

    public Goalkeeper(Person p, string position, GoalkeepingStats gps) : base(p, position, gps)
    {
    }
}

