namespace FootballManagerAPI.Dto
{
    public class FixtureGetDto
    {
        public long Id { get; set; }
        public string FixtureLeagueName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string Venue { get; set; }
        public DateTime? Date { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
