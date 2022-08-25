using MediatR;

namespace Application.Commands
{
    public class SimulateAllFixtures : IRequest
    {
        public long LeagueId { get; set; }
    }
}
