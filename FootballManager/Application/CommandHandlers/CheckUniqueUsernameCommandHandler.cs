using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandHandlers
{
    public class CheckUniqueUsernameCommandHandler : IRequestHandler<CheckUniqueUsernameCommand, Boolean>
    {
        private readonly UserManager<User> _userManager;

        public CheckUniqueUsernameCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(CheckUniqueUsernameCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(request.Username) == null;
        }
    }
}
