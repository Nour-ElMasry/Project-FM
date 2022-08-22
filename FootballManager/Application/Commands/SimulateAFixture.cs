using MediatR;

namespace Application.Commands
{
    public class SimulateAFixture : IRequest
    {
        public long FixtureID { get; set; }
    }
}
