using Application.Filters;
using Application.Pagination;
using Domain.Entities;
using FootballManagerAPI.Filters;
using MediatR;

namespace Application.Queries
{
    public class GetAllPlayers : IRequest<Pager<Player>>
    {
        public PlayerFilter Filter { get; set; }
        public int Page { get; set; }
    }
}
