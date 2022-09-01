using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdatePlayer : IRequest<Player>
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Position { get; set; }
    }
}
