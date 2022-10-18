using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixtureResultById : IRequest<Fixture>
    {
        public long FixtureId { get; set; }
    }
}
