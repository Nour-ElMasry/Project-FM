namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class TeamSheet
{
    [Key]
    public long TeamSheetId { get; set; }

    public int AttackingRating { get; set; } = 0;
    public int DefendingRating { get; set; } = 0;

    [ForeignKey("TeamTacticId")]
    public Tactic TeamTactic { get; set; }

    public TeamSheet()
    {
        TeamTactic = new BalancedTactic();
    }

    public void UpdateRating(List<Player> p)
    {
        if (p.Count > 0)
        {
            AttackingRating = CalculateAttackingRating(p);
            DefendingRating = CalculateDefendingRating(p);
        }
    }

    public int CalculateAttackingRating(List<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        if (playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += pl.PlayerStats.Attacking);
            var rating = (calculation / playersList.Count) + TeamTactic.AttackingWeight;
            return (rating > 100) ? 100 : rating;
        }
        return AttackingRating;
    }

    public int CalculateDefendingRating(List<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Attacker").ToList();
        if (playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += (pl.GetType().Name == "Goalkeeper") ? pl.PlayerStats.Goalkeeping / 5 : pl.PlayerStats.Defending);
            var rating = (calculation / playersList.Count) + TeamTactic.DefendingWeight;
            return (rating > 100) ? 100 : rating;
        }
        return DefendingRating;
    }
}

