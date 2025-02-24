using PortfolioApp.Core.DTOs.Admin.Home;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IHomeRepository
{
    Task<HomeDto> GetHomeInfosAsync();
}
