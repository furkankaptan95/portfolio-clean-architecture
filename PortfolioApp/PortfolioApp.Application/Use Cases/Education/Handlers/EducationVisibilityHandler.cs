using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class EducationVisibilityHandler : IRequestHandler<EducationVisibilityCommand, ServiceResult>
{
    private readonly IEducationRepository _educationRepository;
    public EducationVisibilityHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult> Handle(EducationVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _educationRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        await _educationRepository.UpdateAsync(entity);
        await _educationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Eğitim bilgisi görünürlüğü başarıyla güncellendi.");
    }
}
