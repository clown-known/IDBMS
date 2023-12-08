
using BusinessObject.Models;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Supporters
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            configuration = config;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers.Authorization.ToString().Split(" ").Last();

            if(token != null)
            {
                AttachUserToContext(context, userRepository, token);
            }
            await _next(context);
        }
        private void AttachUserToContext(HttpContext context, IUserRepository userRepository, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["jwt:Key"]!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                string role = jwtToken.Claims.First(claim => claim.Type == "role").Value;
                if (role.Equals("admin")) context.Items["role"] = "admin";
                else
                {
                    var userId = jwtToken.Claims.First(claim => claim.Type == "id").Value;
                    if (userId == null || userId.Equals("")) return;
                    var user = userRepository.GetById(Guid.Parse(userId));
                    context.Items["User"] = user;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
