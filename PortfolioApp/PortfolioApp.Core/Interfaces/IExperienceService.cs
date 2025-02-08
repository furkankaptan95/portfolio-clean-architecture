using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Core.Interfaces;
public interface IExperienceService
{
    Task<ServiceResult> AddAsync(AddExperienceDto dto); 
    Task<ServiceResult<List<ExperienceDto>>> GetAllAsync();
}
