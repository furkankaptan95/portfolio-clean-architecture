using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Queries;
public class GetContactMessagesQuery : IRequest<ServiceResult<List<ContactMessageDto>>> { }

