using MediatR;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class CreateContactMessageHandler : IRequestHandler<CreateContactMessageCommand, ServiceResult>
{
    private readonly IContactMessageRepository _contactMessageRepository;
    public CreateContactMessageHandler(IContactMessageRepository contactMessageRepository)
    {
        _contactMessageRepository = contactMessageRepository;
    }
    public async Task<ServiceResult> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new ContactMessageEntity()
        {
            Name = request.ContactMessage.Name,
            Email = request.ContactMessage.Email,
            Subject = request.ContactMessage.Subject,
            Message = request.ContactMessage.Message,
        };

        await _contactMessageRepository.AddAsync(entity);
        await _contactMessageRepository.SaveChangesAsync();

        return new ServiceResult(true, "Mesaj başarıyla gönderildi.");
    }
}
