using Domain.Entities;

namespace Application.Dto
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Person UserPerson { get; set; }
    }
}
