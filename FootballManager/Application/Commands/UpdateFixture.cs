using MediatR;

namespace Application.Commands
{
    public class UpdateFixture : IRequest
    {
        public long FixtureId { get; set; }
        public string newDate { get; set; }
    }
}
