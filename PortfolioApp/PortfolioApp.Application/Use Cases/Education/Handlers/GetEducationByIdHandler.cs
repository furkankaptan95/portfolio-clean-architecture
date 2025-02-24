using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces.Repositories;


namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class GetEducationByIdHandler : IRequestHandler<GetEducationByIdQuery, ServiceResult<EducationDto>>
{
    private readonly IEducationRepository _educationRepository;
    public GetEducationByIdHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult<EducationDto>> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _educationRepository.GetByIdAsync(request.Id);

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
