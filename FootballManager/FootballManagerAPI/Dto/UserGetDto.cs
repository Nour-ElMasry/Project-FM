using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class UserGetDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public Person UserPerson { get; set; }
    }
}
