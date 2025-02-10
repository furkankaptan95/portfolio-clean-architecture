using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Core.Interfaces;
public interface IAboutMeService
{
    Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto);
    Task<ServiceResult> CreateAboutMeAsync(AddAboutMeMvcDto dto);
    Task<ServiceResult<AboutMeDto>> GetAsync();
    Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeApiDto dto);
}
