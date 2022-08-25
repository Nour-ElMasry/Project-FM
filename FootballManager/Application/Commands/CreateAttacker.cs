using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateAttacker : IRequest<Attacker>
    {
        public long PlayerPersonId { get; set; }
        public string Position { get; set; }
    }
}
