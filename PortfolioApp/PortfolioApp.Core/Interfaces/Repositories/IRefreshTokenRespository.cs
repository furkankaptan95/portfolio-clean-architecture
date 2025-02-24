using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IRefreshTokenRespository : IRepository<RefreshTokenEntity>
{
    Task<RefreshTokenEntity> GetByTokenWithUser(string token);
}
