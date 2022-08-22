using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPlayerById : IRequest<Player>
    {
        public long PlayerId { get; set; }
    }
}
