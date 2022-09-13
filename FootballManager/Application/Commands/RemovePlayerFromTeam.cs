using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class RemovePlayerFromTeam : IRequest<Player>
    {
        public long PlayerId { get; set; }
        public long TeamId { get; set; }
    }
}
