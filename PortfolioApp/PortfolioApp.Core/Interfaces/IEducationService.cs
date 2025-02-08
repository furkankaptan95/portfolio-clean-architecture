using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Core.Interfaces;
public interface IEducationService
{
    Task<ServiceResult> AddAsync(AddEducationDto dto); 
    Task<ServiceResult<List<EducationDto>>> GetAllAsync();
    Task<ServiceResult> DeleteAsync(int id); 
    Task<ServiceResult> UpdateAsync(UpdateEducationDto dto);
    Task<ServiceResult<EducationDto>> GetByIdAsync(int id);
    Task<ServiceResult> ChangeVisibilityAsync(int id);
}
