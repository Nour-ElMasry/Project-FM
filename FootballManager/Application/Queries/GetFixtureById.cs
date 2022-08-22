using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixtureById : IRequest<Fixture>
    {
        public long FixtureId { get; set; }
    }
}
