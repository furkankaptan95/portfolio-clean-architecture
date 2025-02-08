using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Queries;
public class GetContactMessageByIdQuery : IRequest<ServiceResult<ContactMessageDto>>
{
    public int Id { get; }

    public GetContactMessageByIdQuery(int id)
    {
        Id = id;
    }
}

