using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixturesByLeague : IRequest<List<Fixture>>
    {
        public long LeagueId { get; set; }
    }
}
