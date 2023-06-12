namespace Application.Dto
{
    public class CampainLeaguesTeamsDto
    {
        public long LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueLogo { get; set; }
        public List<ShortTeamGetDto> LeagueTeams { get; set; }
    }
}
