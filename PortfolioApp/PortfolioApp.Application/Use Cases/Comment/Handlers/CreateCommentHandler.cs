using MediatR;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateCommentHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new CommentEntity()
        {
            Content = request.Comment.Content,
            BlogPostId = request.Comment.BlogPostId,
            UserId = request.Comment.UserId,
            UnsignedCommenterName = request.Comment.UnsignedCommenterName,
        };

        await _dataDbContext.Comments.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Yorum başarıyla oluşturuldu.");
    }
}
