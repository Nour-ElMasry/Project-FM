using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateUser : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Person UserPerson { get; set; }
    }
}
