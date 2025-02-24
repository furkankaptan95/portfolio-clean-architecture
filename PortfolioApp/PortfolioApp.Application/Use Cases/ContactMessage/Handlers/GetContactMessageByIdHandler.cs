using MediatR;
using PortfolioApp.Application.Use_Cases.ContactMessage.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class GetContactMessageByIdHandler : IRequestHandler<GetContactMessageByIdQuery, ServiceResult<ContactMessageDto>>
{
    private readonly IContactMessageRepository _contactMessageRepository;
    public GetContactMessageByIdHandler(IContactMessageRepository contactMessageRepository)
    {
        _contactMessageRepository = contactMessageRepository;
    }
    public async Task<ServiceResult<ContactMessageDto>> Handle(GetContactMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _contactMessageRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult<ContactMessageDto>(false,"Mesaj bulunamadı.");
        }

        if (entity.ReplyDate is not null)
        {
            return new ServiceResult<ContactMessageDto>(false,"Mesaj daha önceden zaten cevaplanmş!");
        }

        var dto = new ContactMessageDto
        {
            Id = request.Id,
            Email = entity.Email,
            Subject = entity.Subject,
            Message = entity.Message,
            SentDate = entity.SentDate,
            Name = entity.Name,
        };

        return new ServiceResult<ContactMessageDto>(true, null , dto);
    }
}
