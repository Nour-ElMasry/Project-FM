using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Object>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public CreateUserHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Object> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var uniqueCheck = await _userManager.FindByNameAsync(request.Username) == null;

            if (uniqueCheck)
            {
                var person = new Person(request.Name, request.DateOfBirth, request.Country);

                if (request.Image != "" && request.Image != null)
                    person.Image = request.Image;

                var user = new User(person);
                user.UserName = request.Username;

                var create = await _userManager.CreateAsync(user, request.Password);

                if (create.Succeeded) {
                    var dbUser = await _userManager.Users.Include(u => u.UserPerson).FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    var authClaims = new List<Claim>
                    {
                        new Claim("Name", dbUser.UserName)
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

                    var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: authClaims,
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return new
                    {
                        customer = new
                        {
                            userId = dbUser.Id,
                            userUserName = dbUser.UserName,
                            userPerson = dbUser.UserPerson
                        },
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };
                
                }
            }

            return null;
        }
    }
}
