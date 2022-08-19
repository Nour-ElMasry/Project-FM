using Domain.Exceptions;

namespace Domain.Entities;
public abstract class Player
{
    public long PlayerId { get; set; }
    public Person PlayerPerson { get; set; }
    public PlayerStats PlayerStats { get; set; }
    public Team CurrentTeam { get; set; } = null;
    public string Position { get; set; }

    public Player(Person p, string pos)
    {
        if (pos == null)
            throw new ArgumentNullException(nameof(pos));

        if (!PlayerPositions.IsCorrectPosition(pos, GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!");

        PlayerPerson = p ?? throw new ArgumentNullException(nameof(p));
        Position = pos;
        PlayerStats = PlayerStatsFactory.GenerateStats(Position);
    }

    public Player(Person p, string pos, PlayerStats ps)
    {
        if (!PlayerPositions.IsCorrectPosition(pos, GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!");

        PlayerPerson = p ?? throw new ArgumentNullException(nameof(p));
        Position = pos;
        PlayerStats = ps ?? throw new ArgumentNullException(nameof(ps));
    }
}

