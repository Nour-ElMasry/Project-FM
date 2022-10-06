using Application.Filters;
using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllTeams : IRequest<Pager<Team>>
    {
        public TeamFilter Filter { get; set; }
        public int Page { get; set; }
    }
}
