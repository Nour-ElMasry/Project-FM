using MediatR;

namespace Application.Commands
{
    public class AddPlayerToTeam : IRequest
    {
        public long PlayerId { get; set; }
        public long TeamId { get; set; }
    }
}
