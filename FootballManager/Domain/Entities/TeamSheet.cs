namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

public class TeamSheet
{
    [Key]
    public long TeamSheetId { get; set; }

    public int AttackingRating { get; set; } = 0;
    public int DefendingRating { get; set; } = 0;

    [ForeignKey("TeamTacticId")]
    public Tactic TeamTactic { get; set; }

    [ForeignKey("TeamFormationId")]
    public Formation TeamFormation { get; set; } = new Formation(4, 3, 3);

    [ForeignKey("StartingElevenPlayers")]
    public List<Player> StartingEleven { get; set; } = new();

    [ForeignKey("BenchPlayers")]
    public List<Player> Bench { get; set; } = new();

    public TeamSheet()
    {
        TeamTactic = new BalancedTactic();
    }

    public void UpdateRating()
    {
        if (StartingEleven.Count > 0)
        {
            AttackingRating = CalculateAttackingRating();
            DefendingRating = CalculateDefendingRating();
        }
    }

    public int CalculateAttackingRating()
    {
        var calculation = 0;
        var playersList = StartingEleven.Where(p => p.GetType().Name != "Defender" && p.GetType().Name != "Goalkeeper").ToList();
        if (playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += pl.CurrentPlayerStats.Attacking);
            var rating = (calculation / playersList.Count) + TeamTactic.AttackingWeight;
            return (rating > 100) ? 100 : rating;
        }
        return AttackingRating;
    }

    public int CalculateDefendingRating()
    {
        var calculation = 0;
        var playersList = StartingEleven.Where(p => p.GetType().Name != "Attacker").ToList();
        if (playersList.Count > 0)
        {
            playersList.ForEach(pl => calculation += (pl.GetType().Name == "Goalkeeper") ? pl.CurrentPlayerStats.Goalkeeping / 5 : pl.CurrentPlayerStats.Defending);
            var rating = (calculation / playersList.Count) + TeamTactic.DefendingWeight;
            return (rating > 100) ? 100 : rating;
        }
        return DefendingRating;
    }

    public void ChangeFormation(Formation formation, List<Player> teamPlayers)
    {
        TeamFormation = formation;
        this.SetFormationPlayers(teamPlayers);
    }

    public void SetFormationPlayers(List<Player> teamPlayers)
    {
        StartingEleven = new();
        Bench = new();
        var orderedPlayers = teamPlayers.OrderByDescending(p => p.CurrentPlayerStats.OverallRating).ToList();

        try
        {

            var goalkeeper = orderedPlayers.FirstOrDefault(p => p.Position == "Goalkeeper");
            if (goalkeeper == null)
            {
                throw new Exception("No goalkeeper in the team.");
            }


            var defenders = orderedPlayers.Where(p => p.Position == "Defender").Take(TeamFormation.Defenders).ToList();
            if (defenders.Count < TeamFormation.Defenders)
            {
                throw new Exception("Not enough defenders in the team.");
            }

            var midfielders = orderedPlayers.Where(p => p.Position == "Midfielder").Take(TeamFormation.Midfielders).ToList();
            if (midfielders.Count < TeamFormation.Midfielders)
            {
                throw new Exception("Not enough midfielders in the team.");
            }

            var attackers = orderedPlayers.Where(p => p.Position == "Attacker").Take(TeamFormation.Attackers).ToList();
            if (attackers.Count < TeamFormation.Attackers)
            {
                throw new Exception("Not enough attackers in the team.");
            }

            StartingEleven.Add(goalkeeper);
            StartingEleven.AddRange(defenders);
            StartingEleven.AddRange(midfielders);
            StartingEleven.AddRange(attackers);

            var remainingPlayers = orderedPlayers.Except(StartingEleven).ToList();

            var benchGoalkeeper = remainingPlayers.FirstOrDefault(p => p.Position == "Goalkeeper");
            var benchDefenders = remainingPlayers.Where(p => p.Position == "Defender").Take(2).ToList();
            var benchMidfielders = remainingPlayers.Where(p => p.Position == "Midfielder").Take(2).ToList();
            var benchAttackers = remainingPlayers.Where(p => p.Position == "Attacker").Take(2).ToList();

            Bench.Add(benchGoalkeeper);
            Bench.AddRange(benchDefenders);
            Bench.AddRange(benchMidfielders);
            Bench.AddRange(benchAttackers);

            this.UpdateRating();
        }
        catch (Exception ex)
        {
            var currentDefenders = orderedPlayers.Count(p => p.Position == "Defender");
            var currentMidfielders = orderedPlayers.Count(p => p.Position == "Midfielder");
            var currentAttackers = orderedPlayers.Count(p => p.Position == "Attacker");

            TeamFormation.GetAlternativeFormation(currentDefenders, currentMidfielders, currentAttackers);
            this.SetFormationPlayers(teamPlayers);
        }
    }
}

