using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class ManagerGetDto
    {
        public long ManagerId { get; set; }
        public Person ManagerPerson { get; set; }
        public ShortTeamGetDto CurrentTeam { get; set; }
    }
}
