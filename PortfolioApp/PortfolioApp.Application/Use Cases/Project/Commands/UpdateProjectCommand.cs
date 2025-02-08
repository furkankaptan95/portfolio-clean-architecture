using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Application.Use_Cases.Project.Commands;
public class UpdateProjectCommand : IRequest<ServiceResult>
{
    public UpdateProjectApiDto Project { get; set; }
    public UpdateProjectCommand(UpdateProjectApiDto project)
    {
        Project = project;
    }
}
