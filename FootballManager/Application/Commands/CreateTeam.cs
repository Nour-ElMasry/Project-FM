using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateTeam : IRequest<Team>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public string Logo { get; set; }
        public long LeagueId { get; set; }
    }
}
