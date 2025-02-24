using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class UserRepository : Repository<UserEntity, AuthDbContext>, IUserRepository
{
    public UserRepository(AuthDbContext context) : base(context)
    {

    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<UserEntity> GetByEmailWithRefreshTokensAsync(string email)
    {
        var user = await _context.Users.Include(u=>u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<UserEntity> GetByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }
}
