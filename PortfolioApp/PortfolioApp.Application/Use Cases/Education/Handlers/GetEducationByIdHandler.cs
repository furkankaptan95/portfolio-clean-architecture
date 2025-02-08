using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class GetEducationByIdHandler : IRequestHandler<GetEducationByIdQuery, ServiceResult<EducationDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetEducationByIdHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<EducationDto>> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Educations.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult<EducationDto>(false, "Blog Post bulunamadı.");
        }

        var dto = new EducationDto
        {
            Id = request.Id,
            School = entity.School,
            Degree = entity.Degree,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsVisible = entity.IsVisible,
        };

        return new ServiceResult<EducationDto>(true, null , dto);
    }
}
