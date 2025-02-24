using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, ServiceResult>
{
    private readonly IRefreshTokenRespository _refreshTokenRespository;
    public RevokeTokenHandler(IRefreshTokenRespository refreshTokenRespository)
    {
        _refreshTokenRespository = refreshTokenRespository;
    }
    public async Task<ServiceResult> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRespository.GetByTokenWithUser(request.Token);

        if (refreshToken is null)
        {
            return new ServiceResult(true, "Token zaten yok.");
        }

        refreshToken.IsRevoked = DateTime.UtcNow;

        await _refreshTokenRespository.UpdateAsync(refreshToken);
        await _refreshTokenRespository.SaveChangesAsync();

        return new ServiceResult(true);
    }
}
