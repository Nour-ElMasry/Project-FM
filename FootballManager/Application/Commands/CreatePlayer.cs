using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreatePlayer : IRequest<Player>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Position { get; set; }
        public string PlayerRole { get; set; }
    }
}
