using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeleteTeam : IRequest<Team>
    {
        public long TeamId { get; set; }
    }
}
