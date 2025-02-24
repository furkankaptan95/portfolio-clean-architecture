using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class RefreshTokenRespository : Repository<RefreshTokenEntity, AuthDbContext>, IRefreshTokenRespository
{
    public RefreshTokenRespository(AuthDbContext context) : base(context)
    {

    }

    public async Task<RefreshTokenEntity> GetByTokenWithUser(string token)
    {
        var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt =>
              rt.Token == token);
        return refreshToken;
    }
}
