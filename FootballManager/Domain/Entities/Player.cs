using Domain.Exceptions;

namespace Domain.Entities;
public abstract class Player
{
    public long PlayerId { get; set; }

    public long PlayerPersonId { get; set; }
    public Person PlayerPerson { get; set; }

    public long PlayerStatsId { get; set; }
    public PlayerStats PlayerStats { get; set; }

    public long? CurrentTeamId { get; set; }
    public Team? CurrentTeam { get; set; }

    public string Position { get; set; }

    public long PlayerRecordId { get; set; }
    public Record PlayerRecord { get; set; } = new();

    public Player() { }

    public Player(long personId, string pos)
    {
        if (pos == null)
            throw new ArgumentNullException(nameof(pos));

        if (!PlayerPositions.IsCorrectPosition(pos, GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!");

        PlayerPersonId = personId;
        Position = pos;
        PlayerStats = PlayerStatsFactory.GenerateStats(pos);
    }
}

