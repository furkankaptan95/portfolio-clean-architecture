using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ServiceResult<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<ServiceResult<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _projectRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult<ProjectDto>(false, "Proje bulunamadı.");
        }

        var dto = new ProjectDto
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            Title = entity.Title,
            Description = entity.Description,
            IsVisible = entity.IsVisible,
        };

        return new ServiceResult<ProjectDto>(true, null , dto);
    }
}
