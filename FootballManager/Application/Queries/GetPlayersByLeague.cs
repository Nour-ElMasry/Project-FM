using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPlayersByLeague : IRequest<Pager<Player>>
    {
        public int Page { get; set; }
        public long LeagueId { get; set; }
    }
}
