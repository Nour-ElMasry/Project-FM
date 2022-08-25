using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateGoalkeeper : IRequest<Goalkeeper>
    {
        public long PlayerPersonId{ get; set; }
    }
}