namespace FootballManagerAPI.Dto
{
    public class FixtureGetDto
    {
        public long Id { get; set; }
        public ShortLeagueGetDto FixtureLeague { get; set; }
        public ShortTeamGetDto HomeTeam { get; set; }
        public ShortTeamGetDto AwayTeam { get; set; }
        public string Venue { get; set; }
        public DateTime? Date { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
