using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetLeaguesByTeam : IRequest<List<League>>
    {
        public long TeamId { get; set; }
    }
}
