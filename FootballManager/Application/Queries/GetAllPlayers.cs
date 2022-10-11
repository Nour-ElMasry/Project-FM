using Application.Pagination;
using Domain.Entities;
using Application.Filters;
using MediatR;

namespace Application.Queries
{
    public class GetAllPlayers : IRequest<Pager<Player>>
    {
        public PlayerFilter Filter { get; set; }
        public int Page { get; set; }
    }
}
