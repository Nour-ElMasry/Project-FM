using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Formation
{
    [Key]
    public long FormationId { get; set; }

    public int Defenders { get; private set; }
    public int Midfielders { get; private set; }
    public int Attackers { get; private set; }

    private static readonly List<(int Defenders, int Midfielders, int Attackers)> ValidFormations = new()
    {
        (3, 4, 3),
        (3, 5, 2),
        (3, 6, 1),
        (3, 3, 4),
        (4, 4, 2),
        (4, 2, 4),
        (4, 3, 3),
        (4, 5, 1),
        (5, 3, 2),
        (5, 4, 1),
        (5, 2, 3),
        (5, 1, 3),
    };

    public Formation(int defenders, int midfielders, int attackers)
    {
        if (!IsValidFormation(defenders, midfielders, attackers))
        {
            throw new ArgumentException($"Invalid formation ({defenders}-{midfielders}-{attackers}). This formation is not allowed.");
        }
        Defenders = defenders;
        Midfielders = midfielders;
        Attackers = attackers;
    }

    private bool IsValidFormation(int defenders, int midfielders, int attackers)
    {
        return ValidFormations.Contains((defenders, midfielders, attackers));
    }

    public static List<Formation> GetValidFormations()
    {
        return ValidFormations.Select(f => new Formation(f.Defenders, f.Midfielders, f.Attackers)).ToList();
    }

    public void GetAlternativeFormation(int currentDefenders, int currentMidfielders, int currentAttackers)
    {
        var alternativeFormations = ValidFormations
            .Where(f => f.Defenders <= currentDefenders && f.Midfielders <= currentMidfielders && f.Attackers <= currentAttackers)
            .ToList();

        if (!alternativeFormations.Any())
        {
            throw new Exception("No alternative formation found.");
        }

        var alternativeFormation = alternativeFormations.First();

        this.Defenders = alternativeFormation.Defenders;
        this.Midfielders = alternativeFormation.Midfielders;
        this.Attackers = alternativeFormation.Attackers;
    }

}
