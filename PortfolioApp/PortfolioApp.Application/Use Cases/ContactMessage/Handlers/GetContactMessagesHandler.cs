using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.ContactMessage.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class GetContactMessagesHandler : IRequestHandler<GetContactMessagesQuery, ServiceResult<List<ContactMessageDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetContactMessagesHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<ContactMessageDto>>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<ContactMessageDto>();

        var entities = await _dataDbContext.ContactMessages.ToListAsync();

        if (entities is null)
        {
            return new ServiceResult<List<ContactMessageDto>>(true,"Herhangi bir mesaj bulunmuyor.",dtos);
        }

        dtos = entities
       .Select(item => new ContactMessageDto
       {
           Id = item.Id,
           Name = item.Name,
           Email = item.Email,
           Subject = item.Subject,
           Message = item.Message,
           SentDate = item.SentDate,
           IsRead = item.IsRead,
           Reply = item.Reply,
           ReplyDate = item.ReplyDate,
       })
       .ToList();

        return new ServiceResult<List<ContactMessageDto>>(true, null , dtos);
    }
}
