using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class ProjectRepository : Repository<ProjectEntity, DataDbContext>, IProjectRepository
{
    public ProjectRepository(DataDbContext context) : base(context)
    {

    }
}
