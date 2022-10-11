using Domain.Entities;

namespace Application.Dto
{
    public class PlayerGetDto
    {
        public long Id { get; set; }
        public Person PlayerPerson { get; set; }
        public PlayerStats PlayerStats { get; set; }
        public ShortTeamGetDto CurrentTeam { get; set; }
        public string Position { get; set; }
        public Record PlayerRecord { get; set; }
    }
}
