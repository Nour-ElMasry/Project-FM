using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public abstract class Player
{
    [Key]
    public long PlayerId { get; set; }

    [ForeignKey("PlayerPersonId")]
    public Person PlayerPerson { get; set; }

    [ForeignKey("PlayerStatsId")]
    public PlayerStats PlayerStats { get; set; }

    [ForeignKey("CurrentTeamId")]
    public Team CurrentTeam { get; set; }

    public string Position { get; set; }

    [ForeignKey("PlayerRecordId")]
    public Record PlayerRecord { get; set; } = new();

    public Player() { }

    public Player(Person person, string pos)
    {
        if (pos == null)
            throw new ArgumentNullException(nameof(pos));

        if (!PlayerPositions.IsCorrectPosition(pos, GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!");

        PlayerPerson = person;
        Position = pos;
        PlayerStats = PlayerStatsFactory.GenerateStats(pos);
    }

    public void ResetPlayerRecord() {
        PlayerRecord = new Record();
    }
}

