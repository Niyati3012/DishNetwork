using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DishNetwork.Repository.Repository
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration Configuration;
        public JwtService(IConfiguration configuration) { 
            this.Configuration = configuration;
        }
        public  string GenerateJWTAuthetication(UserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.RoleName),
                //new Claim("FirstName", userInfo.FirstName),
                //new Claim("UserId", userInfo.UserId.ToString()),
                new Claim("Email", userInfo.Email),
                new Claim("AspNetUserId", userInfo.AspNetUserId.ToString()),
                //new Claim("RoleId", userInfo.RoleId.ToString()),
                new Claim("Role", userInfo.Role.ToString()),
                new Claim("RoleName", userInfo.RoleName),
                //new Claim(JwtHeaderParameterNames.Jku, userInfo.FirstName),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userInfo.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires =
                //DateTime.Now.AddSeconds(5);
                DateTime.Now.AddDays(5);

            var token = new JwtSecurityToken(
               Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public  bool ValidateToken(string token, out JwtSecurityToken jwtSecurityTokenHandler)
        {
            jwtSecurityTokenHandler = null;

            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                
                jwtSecurityTokenHandler = (JwtSecurityToken)validatedToken;

                if(jwtSecurityTokenHandler != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
