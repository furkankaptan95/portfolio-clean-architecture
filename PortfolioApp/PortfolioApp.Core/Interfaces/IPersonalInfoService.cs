using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;

namespace PortfolioApp.Core.Interfaces;
public interface IPersonalInfoService
{
    Task<ServiceResult> AddAsync(AddPersonalInfoDto dto);
    Task<ServiceResult<PersonalInfoDto>> GetAsync();
    Task<ServiceResult> UpdateAsync(UpdatePersonalInfoDto dto);

}
