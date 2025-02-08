using MediatR;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class ContactMessageService : IContactMessageService
{
    private readonly IMediator _mediator;
    public ContactMessageService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> AddAsync(AddContactMessageDto dto)
    {
        var result = await _mediator.Send(new CreateContactMessageCommand(dto));

        return result;
    }
}
