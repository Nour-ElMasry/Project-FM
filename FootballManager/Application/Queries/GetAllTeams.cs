using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllTeams : IRequest<Pager<Team>>
    {
        public int Page { get; set; }
    }
}
