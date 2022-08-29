using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateMidfielder : IRequest<Midfielder>
    {
        public Person PlayerPerson{ get; set; }
        public string Position { get; set; }
    }
}
