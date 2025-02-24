using MediatR;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class MakeContactMessageReadHandler : IRequestHandler<MakeContactMessageReadCommand, ServiceResult>
{
    private readonly IContactMessageRepository _contactMessageRepository;
    public MakeContactMessageReadHandler(IContactMessageRepository contactMessageRepository)
    {
        _contactMessageRepository = contactMessageRepository;
    }
    public async Task<ServiceResult> Handle(MakeContactMessageReadCommand request, CancellationToken cancellationToken)
    {
        var entity = await _contactMessageRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Mesaj bulunamadı.");
        }

        entity.IsRead = true;

        await _contactMessageRepository.UpdateAsync(entity);
        await _contactMessageRepository.SaveChangesAsync();

        return new ServiceResult(true,"Mesaj durumu - Okundu - olarak güncellendi.");
    }
}