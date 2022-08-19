namespace Domain.Entities;
public class TeamSheet
{
    private Tactic _teamTactic = new BalancedTactic();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public int AttackingRating { get; set; } = 0;
    public int DefendingRating { get; set; } = 0;
    public Tactic TeamTactic { 
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

    public void UpdateRating(ICollection<Player> players) {
        if (players.Count > 0)
        {
            Players = players;
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
        return  (rating > 100) ? 100 : rating;
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

