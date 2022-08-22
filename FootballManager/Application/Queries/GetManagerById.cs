using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetManagerById : IRequest<Manager>
    {
        public long ManagerId { get; set; }
    }
}
