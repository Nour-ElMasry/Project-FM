using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamsByLeague : IRequest<Pager<Team>>
    {
        public long LeagueId { get; set; }
        public int Page { get; set; }
    }
}
