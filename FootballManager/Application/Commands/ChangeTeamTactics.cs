using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class ChangeTeamTactics : IRequest<Team>
    {
        public long TeamId { get; set; }
        public string Tactic { get; set; }
    }
}
