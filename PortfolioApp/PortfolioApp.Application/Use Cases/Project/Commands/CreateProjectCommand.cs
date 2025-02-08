using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Application.Use_Cases.Project.Commands;
public class CreateProjectCommand : IRequest<ServiceResult>
{
    public AddProjectDto Project { get; set; }
    public CreateProjectCommand(AddProjectDto project)
    {
        Project = project;
    }
}
