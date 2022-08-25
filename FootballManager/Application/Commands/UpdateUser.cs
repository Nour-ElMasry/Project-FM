using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdateUser : IRequest
    {
        public long UserId { get; set; }
        public User UpdatedUser { get; set; }
    }
}
