using Domain.Entities;

namespace Application.Dto
{
    public class ShortPlayerGetDto
    {
        public long Id { get; set; }
        public Person PlayerPerson { get; set; }
        public ShortTeamGetDto CurrentTeam { get; set; }
        public PlayerStats PlayerStats { get; set; }
        public string Position { get; set; }
    }
}
