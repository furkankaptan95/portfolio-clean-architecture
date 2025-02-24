using MediatR;
using Microsoft.Extensions.Configuration;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ServiceResult<TokensDto>>
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRespository _refreshTokenRespository;
    public RefreshTokenHandler(IConfiguration configuration, IRefreshTokenRespository refreshTokenRespository)
    {
        _configuration = configuration;
        _refreshTokenRespository = refreshTokenRespository;
    }
    public async Task<ServiceResult<TokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRespository.GetByTokenWithUser(request.Token);

        if (refreshToken is null || refreshToken.ExpireDate < DateTime.UtcNow || refreshToken.IsRevoked is not null || refreshToken.IsUsed is not null)
        {
            return new ServiceResult<TokensDto>(false, "Geçerli bi Refresh Token bulunmuyor.");
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

        await _refreshTokenRespository.AddAsync(newRefreshToken);
        await _refreshTokenRespository.SaveChangesAsync();

        var response = new TokensDto
        {
            JwtToken = jwtString,
            RefreshToken = refreshTokenString
        };

        return new ServiceResult<TokensDto>(true, null, response);
    }
}
