using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class PersonalInfoRepository : Repository<PersonalInfoEntity, DataDbContext>, IPersonalInfoRepository
{
    public PersonalInfoRepository(DataDbContext context) : base(context)
    {

    }
    public async Task<PersonalInfoEntity> CheckAndGetAsync()
    {
        return await _context.PersonalInfo.FirstOrDefaultAsync();
    }
}
