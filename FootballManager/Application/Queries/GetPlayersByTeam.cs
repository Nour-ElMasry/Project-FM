using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPlayersByTeam : IRequest<List<Player>>
    {
        public long TeamId { get; set; }
    }
}
