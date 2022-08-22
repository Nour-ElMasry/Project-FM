using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateGoalkeeper : IRequest<Goalkeeper>
    {
        public Person PlayerPerson { get; set; }
        public GoalkeepingStats Stats { get; set; }
    }
}