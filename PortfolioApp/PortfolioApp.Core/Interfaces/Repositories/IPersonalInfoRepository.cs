using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IPersonalInfoRepository : IRepository<PersonalInfoEntity>
{
    Task<PersonalInfoEntity> CheckAndGetAsync();
}
