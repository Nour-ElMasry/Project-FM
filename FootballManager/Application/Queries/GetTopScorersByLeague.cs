using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTopScorersByLeague : IRequest<List<Player>>
    {
        public long LeagueId { get; set; }
    }
}
