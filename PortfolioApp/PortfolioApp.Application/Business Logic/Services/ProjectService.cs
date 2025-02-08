using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
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
    
    public async Task<ServiceResult> AddAsync(AddProjectDto dto)
    {
        var result = await _mediator.Send(new CreateProjectCommand(dto));

        return result;
    }

    public async Task<ServiceResult<List<ProjectDto>>> GetAllAsync()
    {
        var result = await _mediator.Send(new GetProjectsQuery());

        return result;
    }
}
