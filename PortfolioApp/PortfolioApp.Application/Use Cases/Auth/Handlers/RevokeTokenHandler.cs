using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, ServiceResult>
{
    private readonly AuthDbContext _authDbContext;
    public RevokeTokenHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _authDbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.Token);

        if (refreshToken is null)
        {
            return new ServiceResult(true, "Token zaten yok.");
        }

        refreshToken.IsRevoked = DateTime.UtcNow;

        _authDbContext.RefreshTokens.Update(refreshToken);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true);
    }
}
