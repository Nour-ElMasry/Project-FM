using MediatR;

namespace Application.Commands
{
    public class AddTeamToLeague : IRequest
    {
        public long TeamId { get; set; }
        public long LeagueId { get; set; }
    }
}
