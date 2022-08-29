using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateTeam : IRequest<Team>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
    }
}
