using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixturesByTeam : IRequest<List<Fixture>>
    {
        public long TeamId { get; set; }
    }
}
