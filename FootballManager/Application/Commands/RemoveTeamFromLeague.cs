using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class RemoveTeamFromLeague : IRequest<Team>
    {
        public long TeamId { get; set; }
        public long LeagueId { get; set; }
    }
}
