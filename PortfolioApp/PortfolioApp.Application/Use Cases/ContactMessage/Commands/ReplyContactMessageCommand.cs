using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
public class ReplyContactMessageCommand : IRequest<ServiceResult>
{
    public ReplyContactMessageDto ReplyDto { get; set; }
    public ReplyContactMessageCommand(ReplyContactMessageDto replyDto)
    {
        ReplyDto = replyDto;
    }
}
