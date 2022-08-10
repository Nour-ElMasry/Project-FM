using Domain.Entities.PersonContainer;
using Domain.Exceptions;

namespace Domain.Entities.PlayersContainer.Defence;
public class Defender : Player
{
    public Defender(Person p, string position) : base(p, position)
    {
    }

    public Defender(Person p, string position, DefendingStats dps) : base(p, position, dps)
    {
    }
}

