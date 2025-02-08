using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ServiceResult<ProjectDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetProjectByIdHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

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
