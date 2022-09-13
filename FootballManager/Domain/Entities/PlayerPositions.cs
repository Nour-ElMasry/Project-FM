namespace Domain.Entities;
public static class PlayerPositions
{
    public static bool IsAttacker(string pos) => pos.ToLower() == "attacker";
    public static bool IsDefender(string pos) => pos.ToLower() == "defender";
    public static bool IsMidfielder(string pos) => pos.ToLower() == "midfielder";
    public static bool IsGoalkeeper(string pos) => pos.ToLower() == "goalkeeper";

    public static bool IsCorrectPosition(string pos, string pr)
    {
        var playerRole = pr.ToLower();
        var playerPos = pos.ToLower();

        if (playerRole == playerPos)
            return true;

        return false;
    }
}