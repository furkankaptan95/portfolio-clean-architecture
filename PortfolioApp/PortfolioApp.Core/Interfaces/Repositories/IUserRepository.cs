using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> GetByEmailAsync(string email);
    Task<UserEntity> GetByUsernameAsync(string username);
    Task<UserEntity> GetByEmailWithRefreshTokensAsync(string email);
}
