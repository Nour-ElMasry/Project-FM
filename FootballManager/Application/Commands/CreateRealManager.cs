using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateRealManager : IRequest<RealManager>
    {
        public User UserManager { get; set; }
    }
}
