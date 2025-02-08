using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class GetEducationsHandler : IRequestHandler<GetEducationsQuery, ServiceResult<List<EducationDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetEducationsHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<EducationDto>>> Handle(GetEducationsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<EducationDto>();

        var entities = await _dataDbContext.Educations.ToListAsync();

        if (entities is null)
        {
            return new ServiceResult<List<EducationDto>>(true, "Herhangi bir eğitim bilgisi bulunmuyor.", dtos);
        }

        dtos = entities
       .Select(item => new EducationDto
       {
           Id = item.Id,
           Degree = item.Degree,
           School = item.School,
           StartDate = item.StartDate,
           EndDate = item.EndDate,
           IsVisible = item.IsVisible,
       })
       .ToList();

        return new ServiceResult<List<EducationDto>>(true, null , dtos);
    }
}
