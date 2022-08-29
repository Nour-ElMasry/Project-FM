namespace Domain.Entities;
public class Attacker : Player
{
    public Attacker() { }
    public Attacker(Person p, string position) : base(p, position)
    {
    }
}


