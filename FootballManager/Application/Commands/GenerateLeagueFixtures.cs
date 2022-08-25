using MediatR;

namespace Application.Commands
{
    public class GenerateLeagueFixtures : IRequest
    {
        public long LeagueId { get; set; }
    }
}
