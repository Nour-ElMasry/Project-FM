namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class Player
{
    [Key]
    public long PlayerId { get; set; }

    [ForeignKey("PlayerPersonId")]
    public Person PlayerPerson { get; set; }

    [ForeignKey("CurrentPlayerStatsId")]
    public PlayerStats CurrentPlayerStats { get; set; }

    [ForeignKey("CurrentTeamId")]
    public Team CurrentTeam { get; set; }

    public string Position { get; set; }

    [ForeignKey("PlayerRecordId")]
    public Record PlayerRecord { get; set; } = new();

    public Player() { }

    public Player(Person person)
    {
        PlayerPerson = person;
        Position = GetType().Name;
        CurrentPlayerStats = PlayerStatsFactory.GenerateStats(GetType().Name);
    }

    public void ResetPlayerRecord()
    {
        PlayerRecord = new Record();
    }
}

