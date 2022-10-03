using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllLeagues : IRequest<Pager<League>>
    {
        public int Page { get; set; }
    }
}
