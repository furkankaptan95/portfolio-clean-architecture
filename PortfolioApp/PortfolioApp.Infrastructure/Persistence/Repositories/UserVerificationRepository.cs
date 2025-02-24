using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class UserVerificationRepository : Repository<UserVerificationEntity, AuthDbContext>, IUserVerificationRepository
{
    public UserVerificationRepository(AuthDbContext context) : base(context)
    {

    }

    public async Task<UserVerificationEntity> GetByTokenWithUser(string token)
    {
        var userVerification = await _context.UserVerifications.Include(uv => uv.User).FirstOrDefaultAsync(uv => uv.Token == token);
        return userVerification;
    }
}
