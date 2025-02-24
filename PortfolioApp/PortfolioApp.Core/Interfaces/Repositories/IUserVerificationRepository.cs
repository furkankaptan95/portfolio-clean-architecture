using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IUserVerificationRepository : IRepository<UserVerificationEntity>
{
    Task<UserVerificationEntity> GetByTokenWithUser(string token);
}
