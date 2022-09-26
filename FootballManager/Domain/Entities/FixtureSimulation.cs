namespace Domain.Entities;
public static class FixtureSimulation
{
    private static readonly Random rnd = new();
    private static Fixture fixture;

    private static Team AttackingTeam;
    private static Team DefendingTeam;

    private static double AttackChance;
    public static void Simulate(Fixture f)
    {
        fixture = f;

        var numberOfEvents = rnd.Next(0, 10);


        for (int i = 0; i < numberOfEvents; i++)
        {
            TeamSelection(fixture.HomeTeam, fixture.AwayTeam);
            RandomChance(AttackChance);
        }

        AddGoalsAndGames();
        AddPointsToTeams();
        PlayerStatsAddition();
        fixture.isPlayed = true;
    }

    private static void PlayerStatsAddition()
    {
        var HomeTeamPlayers = fixture.HomeTeam.Players;
        var AwayTeamPlayers = fixture.AwayTeam.Players;

        HomeTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());
        AwayTeamPlayers.ForEach(p => p.PlayerRecord.AddGamePlayed());

        if (fixture.FixtureScore.AwayScore == 0)
        {
            var CleanSheetPlayers = HomeTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }

        if (fixture.FixtureScore.HomeScore == 0)
        {
            var CleanSheetPlayers = AwayTeamPlayers.Where(p => p.GetType().Name != "Attacker").ToList();
            CleanSheetPlayers.ForEach(p => p.PlayerRecord.AddCleanSheet());
        }
    }

    private static void TeamSelection(Team homeTeam, Team AwayTeam)
    {
        var teamRnd = rnd.Next(1, 3);
        switch (teamRnd)
        {
            case 1:
                AttackingTeam = homeTeam;
                DefendingTeam = AwayTeam;
                break;
            case 2:
                AttackingTeam = AwayTeam;
                DefendingTeam = homeTeam;
                break;
            default:
                break;
        }

        var total = (double)AttackingTeam.CurrentTeamSheet.AttackingRating + DefendingTeam.CurrentTeamSheet.DefendingRating;

        AttackChance = Math.Round(((double)AttackingTeam.CurrentTeamSheet.AttackingRating / total) * 100);
    }

    private static void RandomChance(double AttackingSuccessPercentage)
    {
        var range = 100000;

        var chance = rnd.Next(0, range);

        if (chance <= AttackingSuccessPercentage * range)
        {
            if (fixture.HomeTeam == AttackingTeam)
                fixture.FixtureScore.HomeScore += 1;
            else
                fixture.FixtureScore.AwayScore += 1;

            PlayerContributions();
        }
    }

    private static void AddPointsToTeams()
    {
        if (fixture.FixtureScore.HomeScore > fixture.FixtureScore.AwayScore)
        {
            fixture.HomeTeam.CurrentSeasonStats.AddWin();
            fixture.AwayTeam.CurrentSeasonStats.AddLose();
        }
        else if (fixture.FixtureScore.HomeScore < fixture.FixtureScore.AwayScore)
        {
            fixture.HomeTeam.CurrentSeasonStats.AddLose();
            fixture.AwayTeam.CurrentSeasonStats.AddWin();
        }
        else
        {
            fixture.HomeTeam.CurrentSeasonStats.AddDraw();
            fixture.AwayTeam.CurrentSeasonStats.AddDraw();
        }
    }

    private static void AddGoalsAndGames()
    {
        fixture.HomeTeam.CurrentSeasonStats.AddGoalsFor(fixture.FixtureScore.HomeScore);
        fixture.HomeTeam.CurrentSeasonStats.AddGoalsAgainst(fixture.FixtureScore.AwayScore);

        fixture.AwayTeam.CurrentSeasonStats.AddGoalsFor(fixture.FixtureScore.AwayScore);
        fixture.AwayTeam.CurrentSeasonStats.AddGoalsAgainst(fixture.FixtureScore.HomeScore);

        fixture.HomeTeam.CurrentSeasonStats.AddHomeGame();
        fixture.AwayTeam.CurrentSeasonStats.AddAwayGame();
    }

    private static void PlayerContributions()
    {
        var randomNum = rnd.Next(0, 1000);

        var FixtureEvent = new Event() { EventFixture = fixture };

        if (randomNum < 550)
        {
            var player = RandomAttacker();
            FixtureEvent.GoalScorer = player;

            ScoreGoal(player);
        }
        else if (randomNum < 850)
        {
            var player = RandomMidfielder();
            FixtureEvent.GoalScorer = player;

            ScoreGoal(player);
        }
        else
        {
            var player = RandomDefender();
            FixtureEvent.GoalScorer = player;

            ScoreGoal(player);
        }

        if (randomNum < 850)
        {
            var player = RandomPlayer();
            FixtureEvent.GoalAssister = player;

            while(FixtureEvent.GoalScorer == FixtureEvent.GoalAssister)
            {
                player = RandomPlayer();
                FixtureEvent.GoalAssister = player;
            }

            AssistGoal(player);
        }

        fixture.FixtureEvents.Add(FixtureEvent);
    }

    private static void ScoreGoal(Player p) => p.PlayerRecord.AddGoal();
    private static void AssistGoal(Player p) => p.PlayerRecord.AddAssist();

    private static Player RandomAttacker()
    {
        var AttackingPlayers = AttackingTeam.Players.Where(p => p.GetType().Name == "Attacker").ToList();
        return AttackingPlayers[rnd.Next(AttackingPlayers.Count)];
    }

    private static Player RandomMidfielder()
    {
        var MidfieldPlayers = AttackingTeam.Players.Where(p => p.GetType().Name == "Midfielder").ToList();
        return MidfieldPlayers[rnd.Next(MidfieldPlayers.Count)];
    }

    private static Player RandomDefender()
    {
        var DefendingPlayers = AttackingTeam.Players.Where(p => p.GetType().Name == "Defender" || p.GetType().Name == "Goalkeeper").ToList();
        return DefendingPlayers[rnd.Next(DefendingPlayers.Count)];
    }

    private static Player RandomPlayer()
    {
        var players = AttackingTeam.Players;
        return players[rnd.Next(players.Count)];
    }
}

