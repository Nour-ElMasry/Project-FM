using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.CommandHandlers
{
    public class LogInUserHandler : IRequestHandler<LoginUser, Object>
    {
        private readonly UserManager<User> _userManager;

        public LogInUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Object> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(u => u.UserPerson).FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if(user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("idjf12-dnm341-ewppa6-dfh8-vcef5"));

                var token = new JwtSecurityToken(
                        issuer: "https://localhost:7067",
                        audience: "audience",
                        claims: authClaims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new {
                    customer = new { 
                        userId = user.Id,
                        userUserName = user.UserName,
                        userPerson = user.UserPerson
                    },
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }

            return null;
        }
    }
}
