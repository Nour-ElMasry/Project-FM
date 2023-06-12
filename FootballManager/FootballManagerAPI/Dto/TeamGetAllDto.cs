using Domain.Entities;

namespace Application.Dto
{
    public class TeamGetAllDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public string Logo { get; set; }
        public string Tactic { get; set; }
        public Person TeamManager { get; set; }
        public SeasonStats CurrentSeasonStats { get; set; }
        public TeamSheet CurrentTeamSheet { get; set; }
        public ShortLeagueGetDto CurrentLeague { get; set; }
    }
}
