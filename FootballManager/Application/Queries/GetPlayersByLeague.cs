using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPlayersByLeague : IRequest<List<Player>>
    {
        public long LeagueId { get; set; }
    }
}
