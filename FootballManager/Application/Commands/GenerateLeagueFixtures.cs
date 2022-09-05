using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class GenerateLeagueFixtures : IRequest<League>
    {
        public long LeagueId { get; set; }
    }
}
