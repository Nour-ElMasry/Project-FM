using Domain.Entities;
using MediatR;

namespace Application.Commands
{

    public class GenerateGoalkeeper : IRequest<Goalkeeper>
    {
        public Person PlayerPerson { get; set; }
    }
}
