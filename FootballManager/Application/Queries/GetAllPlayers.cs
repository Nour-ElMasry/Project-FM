using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllPlayers : IRequest<Pager<Player>>
    {
        public int Page { get; set; }
    }
}
