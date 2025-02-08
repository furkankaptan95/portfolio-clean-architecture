using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Experience.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class GetExperienceByIdHandler : IRequestHandler<GetExperienceByIdQuery, ServiceResult<ExperienceDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetExperienceByIdHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<ExperienceDto>> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Experiences.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult<ExperienceDto>(false, "Blog Post bulunamadı.");
        }

        var dto = new ExperienceDto
        {
            Id = request.Id,
            Company = entity.Company,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsVisible = entity.IsVisible,
        };

        return new ServiceResult<ExperienceDto>(true,null,dto);
    }
}
