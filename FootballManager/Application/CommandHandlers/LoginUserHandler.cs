using Application.Abstract;
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
    public class LogInUserHandler : IRequestHandler<LoginUser, Object>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public LogInUserHandler(UserManager<User> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<Object> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(u => u.UserPerson).FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if(user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userHasTeam = false;

                if (!userRoles.Contains("Admin"))
                {
                    userHasTeam = await _unitOfWork.ManagerRepository.UserHasTeam(user);
                } 

                var authClaims = new List<Claim>
                {
                    new Claim("Name", user.UserName)
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

                var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: authClaims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new {
                    customer = new { 
                        userId = user.Id,
                        userUserName = user.UserName,
                        userPerson = user.UserPerson,
                        hasTeam = userHasTeam,
                    },
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }

            return null;
        }
    }
}
