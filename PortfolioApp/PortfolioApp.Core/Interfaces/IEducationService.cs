using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Core.Interfaces;
public interface IEducationService
{
    Task<ServiceResult> AddAsync(AddEducationDto dto); 
    Task<ServiceResult<List<EducationDto>>> GetAllAsync();
}
