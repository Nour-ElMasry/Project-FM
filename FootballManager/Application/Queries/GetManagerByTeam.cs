using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetManagerByTeam : IRequest<Manager>
    {
        public long TeamId { get; set; }
    }
}
