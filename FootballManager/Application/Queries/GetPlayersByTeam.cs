using Application.Pagination;
using Domain.Entities;
using Application.Filters;
using MediatR;

namespace Application.Queries
{
    public class GetPlayersByTeam : IRequest<Pager<Player>>
    {
        public PlayerFilter Filter { get; set; }
        public int Page { get; set; }
        public long TeamId { get; set; }
    }
}
