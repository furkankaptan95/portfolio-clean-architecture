using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class DeleteEducationHandler : IRequestHandler<DeleteEducationCommand, ServiceResult>
{
    private readonly IEducationRepository _educationRepository;
    public DeleteEducationHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<ServiceResult> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _educationRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        await _educationRepository.DeleteAsync(entity);
        await _educationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Eğitim bilgisi başarıyla silindi.");
    }
}
