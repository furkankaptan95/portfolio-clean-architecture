using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, ServiceResult>
{
    private readonly IEducationRepository _educationRepository;
    public CreateEducationHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = new EducationEntity()
        {
            Degree = request.Education.Degree,
            StartDate = request.Education.StartDate,
            EndDate = request.Education.EndDate,
            School = request.Education.School,
        };

        await _educationRepository.AddAsync(entity);
        await _educationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Eğitim başarıyla eklendi.");
    }
}
