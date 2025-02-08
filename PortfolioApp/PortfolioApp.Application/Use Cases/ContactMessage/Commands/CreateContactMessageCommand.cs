using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
public class CreateContactMessageCommand : IRequest<ServiceResult>
{
    public AddContactMessageDto ContactMessage { get; set; }
    public CreateContactMessageCommand(AddContactMessageDto contactMessage)
    {
        ContactMessage = contactMessage;
    }
}
