using Domain.Entities;
using FootballManagerAPI.Middleware;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWTAuth_Validation.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration, ILoggerFactory logFactory)
        {
            _next = next;
            _configuration = configuration;
            _logger = logFactory.CreateLogger<JWTMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                _logger.LogInformation("Authenticating...");
                AttachAccountToContext(context, token);
            }
                

            await _next(context);
        }

        private void AttachAccountToContext(HttpContext context, string token)
        {
            try
            {    
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidIssuer = _configuration["Jwt:Issuer"],
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountUserName = jwtToken.Claims.First(x => x.Type == "Name").Value;


                // attach account to context on successful jwt validation
                context.Items["User"] = new User { 
                   UserName = accountUserName,
                };
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
}