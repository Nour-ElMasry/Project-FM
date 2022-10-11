using Domain.Entities;

namespace Application.Dto
{
    public class ManagerGetDto
    {
        public long ManagerId { get; set; }
        public Person ManagerPerson { get; set; }
        public ShortTeamGetDto CurrentTeam { get; set; }
    }
}
