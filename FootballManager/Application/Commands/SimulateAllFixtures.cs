using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class SimulateAllFixtures : IRequest<List<Fixture>>
    {
        public long LeagueId { get; set; }
    }
}
