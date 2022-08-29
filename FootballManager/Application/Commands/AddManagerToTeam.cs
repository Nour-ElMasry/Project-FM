using MediatR;

namespace Application.Commands
{
    public class AddManagerToTeam : IRequest
    {
        public long ManagerId { get; set; }
        public long TeamId { get; set; }
    }
}
