using Domain.Entities;
using MediatR;

namespace Application.Commands
{

    public class GenerateAttacker : IRequest<Attacker>
    {
        public Person PlayerPerson { get; set; }
        public string Position { get; set; }
        public PlayerStats PlayerStats { get; set; }
    }
}
