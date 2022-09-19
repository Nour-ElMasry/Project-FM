using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdateUser : IRequest<User>
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
    }
}
