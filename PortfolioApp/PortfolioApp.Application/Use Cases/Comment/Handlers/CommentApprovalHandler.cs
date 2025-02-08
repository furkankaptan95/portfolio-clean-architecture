using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class CommentApprovalHandler : IRequestHandler<CommentApprovalCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CommentApprovalHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CommentApprovalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Comments.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Yorum bulunamadı.");
        }

        entity.IsApproved = !entity.IsApproved;

        _dataDbContext.Comments.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Yorum onay durumu değiştirildi.");
    }
}
