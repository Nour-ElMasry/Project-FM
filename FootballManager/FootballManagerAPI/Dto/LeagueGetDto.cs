using Domain.Entities;

namespace Application.Dto
{
    public class LeagueGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LeagueLogo { get; set; }
        public Season CurrentSeason { get; set; }
    }
}
