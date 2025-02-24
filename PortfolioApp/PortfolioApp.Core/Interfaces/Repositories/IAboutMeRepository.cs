using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IAboutMeRepository : IRepository<AboutMeEntity>
{
    Task<AboutMeEntity> CheckAndGetAsync();
}
