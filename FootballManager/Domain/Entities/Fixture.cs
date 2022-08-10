namespace Domain.Entities;
public class Fixture
{
    public string Venue { get; set; }
    public DateTime Date { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public Fixture(string venue, DateTime date, Team homeTeam, Team awayTeam)
    {
        Venue = venue;
        Date = date;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;

        HomeTeam.AddFixture(this);
        AwayTeam.AddFixture(this);
    }
}

