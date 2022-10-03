using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPlayersByTeam : IRequest<Pager<Player>>
    {
        public int Page { get; set; }
        public long TeamId { get; set; }
    }
}
