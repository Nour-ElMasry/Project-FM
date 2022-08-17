namespace Domain.Entities;
public static class PlayerPositions
{
    private static readonly List<string> AttackingPos = new() { "LW", "ST", "RW", "CF" };
    private static readonly List<string> MidfieldPos = new() { "CAM", "LM", "CM", "RM", "CDM" };
    private static readonly List<string> DefendingPos = new() { "LWB", "LB", "CB", "RB", "RWB" };
    private static readonly string GoalkeeperPos = "GK";

    public static bool IsAttacker(string pos) => AttackingPos.Contains(pos.ToUpper());
    public static bool IsDefender(string pos) => DefendingPos.Contains(pos.ToUpper());
    public static bool IsMidfielder(string pos) => MidfieldPos.Contains(pos.ToUpper());
    public static bool IsGoalkeeper(string pos) => GoalkeeperPos.Equals(pos.ToUpper());

    public static bool IsCorrectPosition(string pos, string pr)
    {
        var playerRole = pr.ToLower();

        if (playerRole == "attacker")
            return IsAttacker(pos);
        else if (playerRole == "midfielder")
            return IsMidfielder(pos);
        else if (playerRole == "defender")
            return IsDefender(pos);
        else
            return IsGoalkeeper(pos);
    }
}