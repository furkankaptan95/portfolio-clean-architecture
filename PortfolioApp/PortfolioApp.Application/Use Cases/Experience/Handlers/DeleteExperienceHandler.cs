using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, ServiceResult>
{
    private readonly IExperienceRepository _experienceRepository;
    public DeleteExperienceHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }
    public async Task<ServiceResult> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _experienceRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        await _experienceRepository.DeleteAsync(entity);
        await _experienceRepository.SaveChangesAsync();

        return new ServiceResult(true, "Deneyim bilgisi başarıyla silindi.");
    }
}
