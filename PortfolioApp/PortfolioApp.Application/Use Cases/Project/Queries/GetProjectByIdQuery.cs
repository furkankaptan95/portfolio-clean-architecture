using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Application.Use_Cases.Project.Queries;
public class GetProjectByIdQuery : IRequest<ServiceResult<ProjectDto>>
{
    public int Id { get; }

    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }
}