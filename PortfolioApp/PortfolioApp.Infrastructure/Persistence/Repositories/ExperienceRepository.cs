using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;

public class ExperienceRepository : Repository<ExperienceEntity, DataDbContext>, IExperienceRepository
{
    public ExperienceRepository(DataDbContext context) : base(context)
    {

    }
}
