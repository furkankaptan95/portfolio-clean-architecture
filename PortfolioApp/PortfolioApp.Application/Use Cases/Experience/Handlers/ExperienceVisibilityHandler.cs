using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class ExperienceVisibilityHandler : IRequestHandler<ExperienceVisibilityCommand, ServiceResult>
{
    private readonly IExperienceRepository _experienceRepository;
    public ExperienceVisibilityHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }
    public async Task<ServiceResult> Handle(ExperienceVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _experienceRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        await _experienceRepository.UpdateAsync(entity);
        await _experienceRepository.SaveChangesAsync();

        return new ServiceResult(true, "Deneyim bilgisi görünürlüğü başarıyla güncellendi.");
    }
}
