using Domain.Enum;
using Domain.Exceptions;

namespace Domain.Entities;
public class Player : Person
{

    public Player(string role, string position, string name, string birthDate, string nationality, string photo) : base(name, birthDate, nationality, photo)
    {
        if (!System.Enum.TryParse(role, out Roles pRole))
            throw new IncorectRoleException("Assigned role does not exist!");

        if (!System.Enum.TryParse(position, out Positions pPosition))
            throw new IncorrectPositionException("Assigned Position does not exist!");

        Role = pRole;
        Position = pPosition;
        PlayerStats = new PlayerStats("Default");
        PlayerRecord = new();
    }

    public PlayerStats PlayerStats { get; set; }
    public PlayerRecord PlayerRecord { get; set; }
    public Team? CurrentTeam { get; set; }
    public Roles Role { get; set; }
    public Positions Position { get; set; }  
}

