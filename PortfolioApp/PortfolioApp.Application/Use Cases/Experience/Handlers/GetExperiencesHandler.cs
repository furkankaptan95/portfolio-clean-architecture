using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Experience.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class GetExperiencesHandler : IRequestHandler<GetExperiencesQuery, ServiceResult<List<ExperienceDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetExperiencesHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<ExperienceDto>>> Handle(GetExperiencesQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<ExperienceDto>();

        var entities = await _dataDbContext.Experiences.ToListAsync();

        if (entities is null)
        {
            return new ServiceResult<List<ExperienceDto>>(true, "Herhangi bir deneyim bilgisi bulunmuyor.", dtos);
        }

        dtos = entities
       .Select(item => new ExperienceDto
       {
           Id = item.Id,
           Company = item.Company,
           Description = item.Description,
           StartDate = item.StartDate,
           EndDate = item.EndDate,
           IsVisible = item.IsVisible,
           Title = item.Title,
       })
       .ToList();

        return new ServiceResult<List<ExperienceDto>>(true, null, dtos);
    }
}
