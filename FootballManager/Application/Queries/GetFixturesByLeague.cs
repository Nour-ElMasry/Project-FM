using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixturesByLeague : IRequest<Pager<Fixture>>
    {
        public int Page { get; set; }
        public long LeagueId { get; set; }
    }
}
