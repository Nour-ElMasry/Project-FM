using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class AuthUser : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
