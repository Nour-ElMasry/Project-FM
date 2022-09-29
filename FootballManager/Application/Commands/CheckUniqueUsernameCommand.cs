using MediatR;

namespace Application.Commands
{
    public class CheckUniqueUsernameCommand : IRequest<Boolean>
    {
        public string Username { get; set; }
    }
}
