using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.QueryHandlers
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserName, User>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByUserNameHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(GetUserByUserName request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(request.UserName);
        }
    }
}
