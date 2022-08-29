using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class TeamSheet
{
    [Key]
    public long TeamSheetId { get; set; }
    public List<Player> TeamSheetPlayers { get; set; }
    public int AttackingRating { get; set; } = 0;
    public int DefendingRating { get; set; } = 0;

    [ForeignKey("TeamTacticId")]
    public Tactic TeamTactic { get; set; }

    public TeamSheet() {
        TeamTactic = new BalancedTactic();
        TeamSheetPlayers = new();
    }

    public void UpdateRating(List<Player> p)
    {
        if (p.Count > 0)
        {
            TeamSheetPlayers = p;
            AttackingRating = CalculateAttackingRating();
            DefendingRating = CalculateDefendingRating();
        }
    }

    public int CalculateAttackingRating()
    {
        var calculation = 0;
        var playersList = TeamSheetPlayers.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        if (playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += pl.PlayerStats.Attacking);
            var rating = (calculation / playersList.Count) + TeamTactic.AttackingWeight;
            return (rating > 100) ? 100 : rating;
        }
        return AttackingRating;
    }

    public int CalculateDefendingRating()
    {
        var calculation = 0;
        var playersList = TeamSheetPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
        if(playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += (pl.GetType().Name == "Goalkeeper") ? pl.PlayerStats.Goalkeeping / 5 : pl.PlayerStats.Defending);
            var rating = (calculation / playersList.Count) + TeamTactic.DefendingWeight;
            return (rating > 100) ? 100 : rating;
        }
       return DefendingRating;
    }
}

