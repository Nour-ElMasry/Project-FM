using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamsByLeague : IRequest<List<Team>>
    {
        public long LeagueId { get; set; }
    }
}
