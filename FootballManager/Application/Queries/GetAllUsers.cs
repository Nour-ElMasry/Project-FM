using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllUsers : IRequest<Pager<User>>
    {
        public int Page { get; set; }
    }
}
