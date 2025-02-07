using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Core.Interfaces;
public interface IAboutMeService
{
    Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto);
    Task<ServiceResult<AboutMeDto>> GetAsync();
}
