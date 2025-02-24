using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class AboutMeRepository : Repository<AboutMeEntity, DataDbContext>, IAboutMeRepository
{
    public AboutMeRepository(DataDbContext context) : base(context)
    {

    }

    public async Task<AboutMeEntity> CheckAndGetAsync()
    {
        return await _context.AboutMe.FirstOrDefaultAsync();
    }
}
