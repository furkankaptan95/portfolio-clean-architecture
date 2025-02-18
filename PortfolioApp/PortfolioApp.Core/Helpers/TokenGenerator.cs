using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioApp.Core.Helpers;
public static class TokenGenerator
{
    public static string GenerateJwtToken(UserEntity user, IConfiguration _configuration)
    {
        var claims = new List<Claim>
           {
                new Claim("userId",user.Id.ToString()),
                new Claim("email",user.Email),
                new Claim(JwtClaimTypes.Role, user.Role),
                new Claim("name",user.Username),
            };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        int expireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"]);

        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims = claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

        return tokenString;
    }
    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
