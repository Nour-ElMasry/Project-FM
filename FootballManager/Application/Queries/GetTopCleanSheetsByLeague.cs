using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTopCleanSheetsByLeague : IRequest<List<Player>>
    {
        public long LeagueId { get; set; }
    }
}
