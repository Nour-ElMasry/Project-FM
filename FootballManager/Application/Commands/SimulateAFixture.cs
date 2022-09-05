using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class SimulateAFixture : IRequest<Fixture>
    {
        public long FixtureID { get; set; }
        public long LeagueId { get; set; }
    }
}
