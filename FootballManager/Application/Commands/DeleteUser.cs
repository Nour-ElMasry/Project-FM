using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeleteUser : IRequest<User>
    {
        public long UserId { get; set; }
    }
}
