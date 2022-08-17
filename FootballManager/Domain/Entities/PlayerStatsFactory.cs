namespace Domain.Entities;
public static class PlayerStatsFactory
{
    private static readonly Random rnd = new();
    public static PlayerStats GenerateStats(string position)
    {
        if (PlayerPositions.IsGoalkeeper(position))
            return new GoalkeepingStats(
                    rnd.Next(10, 20),
                    rnd.Next(30, 60),
                    rnd.Next(10, 40),
                    rnd.Next(75, 100)
                );

        else if (PlayerPositions.IsDefender(position))
            return new DefendingStats(
                    rnd.Next(20, 50),
                    rnd.Next(40, 70),
                    rnd.Next(75, 100),
                    rnd.Next(10, 20)
                );

        else if (PlayerPositions.IsMidfielder(position))
            return new MidfieldStats(
                    rnd.Next(60, 85),
                    rnd.Next(75, 100),
                    rnd.Next(30, 60),
                    rnd.Next(10, 20)
                );

        else
            return new AttackingStats(
                    rnd.Next(75, 100),
                    rnd.Next(60, 100),
                    rnd.Next(20, 50),
                    rnd.Next(10, 20)
                );
    }
}
