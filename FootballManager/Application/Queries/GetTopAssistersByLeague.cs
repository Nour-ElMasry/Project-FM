using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTopAssistersByLeague : IRequest<List<Player>>
    {
        public long LeagueId { get; set; }
    }
}
