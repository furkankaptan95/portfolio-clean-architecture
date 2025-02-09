using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Infrastructure.Persistence.DbContexts;


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

        string jwt = TokenGenerator.GenerateJwtToken(user, _configuration);

        string refreshTokenString = TokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshTokenEntity
        {
            Token = refreshTokenString,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddDays(7),
        };

        await _authDbContext.RefreshTokens.AddAsync(refreshToken);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        var tokensDto = new TokensDto
        {
            JwtToken = jwt,
            RefreshToken = refreshTokenString
        };

        return new ServiceResult<TokensDto>(true,"Giriş başarılı",tokensDto);
    }
}
