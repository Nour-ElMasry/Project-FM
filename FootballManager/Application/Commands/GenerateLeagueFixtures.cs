using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class GenerateLeagueFixtures : IRequest<List<Fixture>>
    {
        public long LeagueId { get; set; }
    }
}
