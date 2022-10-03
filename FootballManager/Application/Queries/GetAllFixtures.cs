using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllFixtures : IRequest<Pager<Fixture>>
    {
        public int Page { get; set; }
    }
}
