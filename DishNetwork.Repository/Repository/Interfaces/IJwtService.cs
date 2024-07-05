
using DishNetwork.Entity.ViewModels;
using System.IdentityModel.Tokens.Jwt;


namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJWTAuthetication(UserInfo userInfo);
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityTokenHandler);
    }
}
