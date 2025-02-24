using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class EducationRepository : Repository<EducationEntity, DataDbContext>, IEducationRepository
{
    public EducationRepository(DataDbContext context) : base(context)
    {

    }
}
