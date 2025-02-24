using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class GetEducationsHandler : IRequestHandler<GetEducationsQuery, ServiceResult<List<EducationDto>>>
{
    private readonly IEducationRepository _educationRepository;
    public GetEducationsHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult<List<EducationDto>>> Handle(GetEducationsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<EducationDto>();

        var entities = await _educationRepository.GetAllAsync();

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
