namespace Domain.Entities;
public class TeamSheet
{
    private Tactic _teamTactic = new BalancedTactic();
    public List<Player> Players { get; set; } = new();
    public int AttackingRating { get; set; } = 0;
    public int DefendingRating { get; set; } = 0;
    public Tactic TeamTactic
    {
        get => _teamTactic;
        set
        {
            _teamTactic = value;
            UpdateRating(Players);
        }
    }

    public TeamSheet()
    {
        TeamTactic = new BalancedTactic();
    }

    public void UpdateRating(List<Player> p)
    {
        if (p.Count > 0)
        {
            Players = p;
            AttackingRating = CalculateAttackingRating();
            DefendingRating = CalculateDefendingRating();
        }
    }

    public int CalculateAttackingRating()
    {
        var calculation = 0;
        var playersList = Players.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        playersList.ForEach(pl => calculation += pl.PlayerStats.Attacking);
        var rating = (calculation / playersList.Count) + TeamTactic.AttackingWeight;
        return (rating > 100) ? 100 : rating;
    }

    public int CalculateDefendingRating()
    {
        var calculation = 0;
        var playersList = Players.Where(p => p.GetType().Name != "Attacker").ToList();
        playersList.ForEach(pl => calculation += (pl.GetType().Name == "Goalkeeper") ? pl.PlayerStats.Goalkeeping / 5 : pl.PlayerStats.Defending);
        var rating = (calculation / playersList.Count) + TeamTactic.DefendingWeight;
        return (rating > 100) ? 100 : rating;
    }
}

