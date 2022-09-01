using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdateFixture : IRequest<Fixture>
    {
        public long FixtureId { get; set; }
        public string newDate { get; set; }
    }
}
