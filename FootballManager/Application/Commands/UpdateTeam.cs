using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdateTeam : IRequest<Team>
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
    }
}
