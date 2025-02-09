using IdentityModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class LoginHandler : IRequestHandler<LoginCommand, ServiceResult<TokensDto>>
{
    private readonly AuthDbContext _authDbContext; 
    private readonly IConfiguration _configuration;
    public LoginHandler(AuthDbContext authDbContext, IConfiguration configuration)
    {
        _authDbContext = authDbContext;
        _configuration = configuration;
    }
    public async Task<ServiceResult<TokensDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
       var user = await _authDbContext.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == request.Login.Email);

        if (user == null)
        {
            return new ServiceResult<TokensDto>(false, "Hatalı kullanıcı adı veya şifre!");
        }

        if (!HashingHelper.VerifyPasswordHash(request.Login.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResult<TokensDto>(false, "Hatalı kullanıcı adı veya şifre!");
        }

        if (HashingHelper.VerifyPasswordHash(request.Login.Password, user.PasswordHash, user.PasswordSalt) && user.IsActive == false)
        {
            return new ServiceResult<TokensDto>(false, "Lütfen hesabınızı aktive edin.");
        }

        user.RefreshTokens.ToList().ForEach(t => t.IsRevoked = DateTime.UtcNow);

        string jwt = GenerateJwtToken(user);

        string refreshTokenString = GenerateRefreshToken();

        var refreshToken = new RefreshTokenEntity
        {
            Token = refreshTokenString,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddDays(7),
        };

        await _authDbContext.RefreshTokens.AddAsync(refreshToken);
        await _authDbContext.SaveChangesAsync();

        var tokensDto = new TokensDto
        {
            JwtToken = jwt,
            RefreshToken = refreshTokenString
        };

        return new ServiceResult<TokensDto>(true,"Giriş başarılı",tokensDto);
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var claims = new List<Claim>
           {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()),
                new Claim(JwtClaimTypes.Email,user.Email),
                new Claim(JwtClaimTypes.Role, user.Role),
                new Claim(JwtClaimTypes.Name,user.Username),
                new Claim("user-img", user.ImageUrl ?? "default.png"),
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
    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
