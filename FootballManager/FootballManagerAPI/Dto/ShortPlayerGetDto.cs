using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class ShortPlayerGetDto
    {
        public long Id { get; set; }
        public Person PlayerPerson { get; set; }
        public ShortTeamGetDto CurrentTeam { get; set; }
        public string Position { get; set; }
    }
}
