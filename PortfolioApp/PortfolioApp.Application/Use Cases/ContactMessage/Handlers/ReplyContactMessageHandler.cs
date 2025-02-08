using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class ReplyContactMessageHandler : IRequestHandler<ReplyContactMessageCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public ReplyContactMessageHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(ReplyContactMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.ContactMessages.FirstOrDefaultAsync(x => x.Id == request.ReplyDto.Id);

        if (entity is null)
        {
            return new ServiceResult(false,"Mesaj bulunamadı.");
        }

        entity.IsRead = true;
        entity.Reply = request.ReplyDto.ReplyMessage;
        entity.ReplyDate = DateTime.UtcNow;

        _dataDbContext.ContactMessages.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Mesaj başarıyla yanıtlandı.");
    }
}
