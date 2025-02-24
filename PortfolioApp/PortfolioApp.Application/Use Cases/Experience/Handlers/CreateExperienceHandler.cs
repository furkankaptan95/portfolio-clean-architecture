using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, ServiceResult>
{
    private readonly IExperienceRepository _experienceRepository;
    public CreateExperienceHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }
    public async Task<ServiceResult> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExperienceEntity()
        {
            Title = request.Experience.Title,
            Company = request.Experience.Company,
            StartDate = request.Experience.StartDate,
            EndDate = request.Experience.EndDate,
            Description = request.Experience.Description,
        };

        await _experienceRepository.AddAsync(entity);
        await _experienceRepository.SaveChangesAsync();

        return new ServiceResult(true, "Deneyim başarıyla eklendi.");
    }
}
