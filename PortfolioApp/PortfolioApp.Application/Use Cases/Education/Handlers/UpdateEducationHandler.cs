using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class UpdateEducationHandler : IRequestHandler<UpdateEducationCommand, ServiceResult>
{
    private readonly IEducationRepository _educationRepository;
    public UpdateEducationHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _educationRepository.GetByIdAsync(request.Education.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        entity.School = request.Education.School;
        entity.EndDate = request.Education.EndDate;
        entity.StartDate = request.Education.StartDate;
        entity.Degree = request.Education.Degree;

        await _educationRepository.UpdateAsync(entity);
        await _educationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Eğitim bilgisi başarıyla güncellendi.");
    }
}
