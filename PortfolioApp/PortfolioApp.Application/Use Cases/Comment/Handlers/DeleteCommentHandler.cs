using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public DeleteCommentHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Comments.FirstOrDefaultAsync(x => x.Id == request.Id); 
        
        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz yorum bulunamadı.");
        }

        _dataDbContext.Comments.Remove(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Yorum başarıyla silindi.");
    }
}
