using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateAttacker : IRequest<Attacker>
    {
        public Person PlayerPerson { get; set; }
        public string Position { get; set; }
    }
}
