namespace Domain.Entities;
public class TeamSheet
{
    public int AttackingRating { get; set; }
    public int DefendingRating { get; set; }
    public Tactic TeamTactic { get; set; }

    public TeamSheet()
    {
        TeamTactic = new BalancedTactic();
    }

    public void UpdateRating(ICollection<Player> players) {
        AttackingRating = CalculateAttackingRating(players);
        DefendingRating = CalculateDefendingRating(players);
    }

    public int CalculateAttackingRating(ICollection<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        playersList.ForEach(pl => calculation += pl.PlayerStats.Attacking);
        return (calculation / playersList.Count) + TeamTactic.AttackingWeight;
    }

    public int CalculateDefendingRating(ICollection<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Attacker").ToList();
        playersList.ForEach(pl => calculation += (pl.GetType().Name == "Goalkeeper") ? pl.PlayerStats.Goalkeeping : pl.PlayerStats.Defending);
        return (calculation / playersList.Count) + TeamTactic.DefendingWeight;
    }
}

