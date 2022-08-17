namespace Domain.Entities;
public class TeamSheet
{
    public List<Player> StartingPlayers { get; set; }
    public int AttackingRating { get; set; }
    public int DefendingRating { get; set; }
    public Tactic TeamTactic { get; set; }

    public TeamSheet()
    {
        StartingPlayers = new();
        TeamTactic = new BalancedTactic();
        AttackingRating = 0;
        DefendingRating = 0;
    }

    public TeamSheet(List<Player> players)
    {
        if (players == null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count > 11)
            throw new ArgumentException("Can't have more than 11 players in starting lineup");

        StartingPlayers = players;
        TeamTactic = new BalancedTactic();
        AttackingRating = CalculateAttackingRating(StartingPlayers);
        DefendingRating = CalculateDefendingRating(StartingPlayers);
    }

    public int CalculateAttackingRating(List<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        playersList.ForEach(pl => calculation += pl.PlayerStats.Attacking);
        return calculation / playersList.Count * 100 + TeamTactic.AttackingWeight;
    }

    public int CalculateDefendingRating(List<Player> p)
    {
        var calculation = 0;
        var playersList = p.Where(p => p.GetType().Name != "Attacker").ToList();
        playersList.ForEach(pl => calculation += pl.PlayerStats.Defending);
        return calculation / playersList.Count * 100 + TeamTactic.DefendingWeight;
    }
}

