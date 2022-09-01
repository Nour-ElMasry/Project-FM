using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class LeagueGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Season CurrentSeason { get; set; }
    }
}
