using MediatR;

namespace Application.Commands
{
    public class RemoveTeamFromLeague : IRequest
    {
        public long TeamId { get; set; }
        public long LeagueId { get; set; }
    }
}
