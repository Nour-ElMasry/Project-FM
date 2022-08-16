using Domain.Entities.PersonContainer;
using Domain.Entities.TeamContainer;
using Domain.Exceptions;

namespace Domain.Entities.PlayersContainer;
public abstract class Player
{

    public Player(Person p, string pos)
    {
        if (pos == null)
            throw new ArgumentNullException(nameof(pos));

        if (!PlayerPositions.IsCorrectPosition(pos, this.GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!"); 

        PlayerPerson = p ?? throw new ArgumentNullException(nameof(p));
        Position = pos;
        PlayerStats = PlayerStatsFactory.GenerateStats(Position);
    }

    public Player(Person p, string pos, PlayerStats ps)
    {
        if (!PlayerPositions.IsCorrectPosition(pos, this.GetType().Name))
            throw new IncorrectPositionException("Incorrect position assigned to role!");

        PlayerPerson = p ?? throw new ArgumentNullException(nameof(p));
        Position = pos;
        PlayerStats = ps ?? throw new ArgumentNullException(nameof(ps));
    }

    public Person PlayerPerson { get; set; }
    public PlayerStats PlayerStats { get; set; }
    public Team? CurrentTeam { get; set; }
    public string Position { get; set; }

    public override string? ToString()
    {
        return $"Player details: {PlayerPerson}, \nPlayer Position: {Position}, \nPlayer Stats: {PlayerStats}, \nTeam: {(CurrentTeam != null ? CurrentTeam.Name : "No Team")}\n";
    }
}

