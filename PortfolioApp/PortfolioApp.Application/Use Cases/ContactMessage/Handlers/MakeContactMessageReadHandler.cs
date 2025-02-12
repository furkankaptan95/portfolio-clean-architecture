using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class MakeContactMessageReadHandler : IRequestHandler<MakeContactMessageReadCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public MakeContactMessageReadHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(MakeContactMessageReadCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.ContactMessages.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Mesaj bulunamadı.");
        }

        entity.IsRead = true;

        _dataDbContext.ContactMessages.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true,"Mesaj durumu - Okundu - olarak güncellendi.");
    }
}