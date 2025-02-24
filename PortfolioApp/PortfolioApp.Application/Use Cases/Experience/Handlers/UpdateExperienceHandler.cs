using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, ServiceResult>
{
    private readonly IExperienceRepository _experienceRepository;
    public UpdateExperienceHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }
    public async Task<ServiceResult> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _experienceRepository.GetByIdAsync(request.Experience.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        entity.Company = request.Experience.Company;
        entity.EndDate = request.Experience.EndDate;
        entity.StartDate = request.Experience.StartDate;
        entity.Title = request.Experience.Title;
        entity.Description = request.Experience.Description;

        await _experienceRepository.UpdateAsync(entity);
        await _experienceRepository.SaveChangesAsync();

        return new ServiceResult(true, "Deneyim bilgisi başarıyla güncellendi.");
    }
}
