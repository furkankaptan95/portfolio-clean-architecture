using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class ProjectService : IProjectService
{
    private readonly IMediator _mediator;
    public ProjectService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<ServiceResult> AddAsync(AddApiProjectDto dto)
    {
        var result = await _mediator.Send(new CreateProjectCommand(dto));

        return result;
    }

    public Task<ServiceResult> AddAsync(AddMvcProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var result = await _mediator.Send(new ProjectVisibilityCommand(id));

        return result;
    }

    public async Task<ServiceResult> DeleteAsync(DeleteProjectDto dto)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(dto.Id));

        return result;
    }

    public async Task<ServiceResult<List<ProjectDto>>> GetAllAsync()
    {
        var result = await _mediator.Send(new GetProjectsQuery());

        return result;
    }

    public async Task<ServiceResult<ProjectDto>> GetByIdAsync(int id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));

        return result;
    }

    public async Task<ServiceResult> UpdateAsync(UpdateProjectApiDto dto)
    {
        var result = await _mediator.Send(new UpdateProjectCommand(dto));

        return result;
    }

    public Task<ServiceResult> UpdateAsync(UpdateProjectMVCDto dto)
    {
        throw new NotImplementedException();
    }
}
