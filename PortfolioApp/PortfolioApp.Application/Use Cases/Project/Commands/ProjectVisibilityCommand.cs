using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Project.Commands;
public class ProjectVisibilityCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }

    public ProjectVisibilityCommand(int id)
    {
        Id = id;
    }
}
