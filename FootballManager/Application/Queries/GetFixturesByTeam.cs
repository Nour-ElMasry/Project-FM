using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetFixturesByTeam : IRequest<Pager<Fixture>>
    {
        public int Page { get; set; }
        public long TeamId { get; set; }
    }
}
