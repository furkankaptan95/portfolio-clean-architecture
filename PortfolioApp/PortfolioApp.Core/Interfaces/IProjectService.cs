using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Core.Interfaces;
public interface IProjectService
{
    Task<ServiceResult> AddAsync(AddApiProjectDto dto);
    Task<ServiceResult> AddAsync(AddMvcProjectDto dto);
    Task<ServiceResult<List<ProjectDto>>> GetAllAsync();
    Task<ServiceResult> DeleteAsync(DeleteProjectDto dto);
    Task<ServiceResult> UpdateAsync(UpdateProjectApiDto dto);
    Task<ServiceResult> UpdateAsync(UpdateProjectMVCDto dto);
    Task<ServiceResult<ProjectDto>> GetByIdAsync(int id);
    Task<ServiceResult> ChangeVisibilityAsync(int id);
}
