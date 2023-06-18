using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class RetireUserTeam : IRequest<Team>
    {
        public string UserId { get; set; }
    }
}
