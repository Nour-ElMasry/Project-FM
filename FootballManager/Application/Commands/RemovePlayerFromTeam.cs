using MediatR;

namespace Application.Commands
{
    public class RemovePlayerFromTeam : IRequest
    {
        public long PlayerId { get; set; }
        public long TeamId { get; set; }
    }
}
