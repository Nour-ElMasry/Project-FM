using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly UserManager<User> _userManager;

        public CreateUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var uniqueCheck = await _userManager.FindByNameAsync(request.Username) == null;

            if (uniqueCheck)
            {
                var person = new Person(request.Name, request.DateOfBirth, request.Country);

                if (request.Image != "")
                    person.Image = request.Image;

                var user = new User(person);
                user.UserName = request.Username;

                var create = await _userManager.CreateAsync(user, request.Password);
         
                if(create.Succeeded)
                    return user;
            }

            return null;
        }
    }
}
