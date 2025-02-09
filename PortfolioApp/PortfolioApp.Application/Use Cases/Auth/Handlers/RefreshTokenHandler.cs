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
public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ServiceResult<TokensDto>>
{
    private readonly AuthDbContext _authDbContext;
    private readonly IConfiguration _configuration;
    public RefreshTokenHandler(AuthDbContext authDbContext, IConfiguration configuration)
    {
        _authDbContext = authDbContext;
        _configuration = configuration;
    }
    public async Task<ServiceResult<TokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _authDbContext.RefreshTokens.Where(rt =>
              rt.Token == request.Token &&
              rt.ExpireDate > DateTime.UtcNow &&
              rt.IsRevoked == null &&
              rt.IsUsed == null).Include(rt => rt.User).FirstOrDefaultAsync();

        if (refreshToken is null)
        {
            return new ServiceResult<TokensDto>(false,"Geçerli bi Refresh Token bulunmuyor.");
        }

        refreshToken.IsUsed = DateTime.UtcNow;

        string jwtString = TokenGenerator.GenerateJwtToken(refreshToken.User, _configuration);

        string refreshTokenString = TokenGenerator.GenerateRefreshToken();

        var newRefreshToken = new RefreshTokenEntity
        {
            Token = refreshTokenString,
            UserId = refreshToken.User.Id,
            ExpireDate = DateTime.UtcNow.AddDays(7),
        };

        await _authDbContext.RefreshTokens.AddAsync(newRefreshToken);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        var response = new TokensDto
        {
            JwtToken = jwtString,
            RefreshToken = refreshTokenString
        };

        return new ServiceResult<TokensDto>(true, null, response);
    }
}
