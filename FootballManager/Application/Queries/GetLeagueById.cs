using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetLeagueById : IRequest<League>
    {
        public long LeagueId { get; set; }
    }
}
