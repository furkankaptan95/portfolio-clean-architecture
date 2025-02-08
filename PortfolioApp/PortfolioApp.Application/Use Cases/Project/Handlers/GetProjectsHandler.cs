using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, ServiceResult<List<ProjectDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetProjectsHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<ProjectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<ProjectDto>();

        var entities = await _dataDbContext.Projects.ToListAsync();


        if (entities is null)
        {
            return new ServiceResult<List<ProjectDto>>(true, "Herhangi bir proje bilgisi bulunmuyor.", dtos);
        }

        dtos = entities
        .Select(item => new ProjectDto
        {
            Id = item.Id,
            ImageUrl = item.ImageUrl,
            Description = item.Description,
            IsVisible = item.IsVisible,
            Title = item.Title,
        })
        .ToList();

        return new ServiceResult<List<ProjectDto>>(true, null, dtos);
    }
}
