namespace Domain.Entities;
public class Midfielder : Player
{
    public Midfielder(Person p, string position) : base(p, position)
    {
    }

    public Midfielder(Person p, string position, MidfieldStats mps) : base(p, position, mps)
    {
    }
}

