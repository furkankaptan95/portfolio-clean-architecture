using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class ProjectService : IProjectService
{
    public async Task<ServiceResult> AddAsync(AddProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ProjectDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<ProjectDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateProjectApiDto dto)
    {
        throw new NotImplementedException();
    }
}
