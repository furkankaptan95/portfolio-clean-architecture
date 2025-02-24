using MediatR;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, ServiceResult>
{
    private readonly ICommentRepository _commentRepository;
    public CreateCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<ServiceResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new CommentEntity()
        {
            Content = request.Comment.Content,
            BlogPostId = request.Comment.BlogPostId,
            UserId = request.Comment.UserId,
            SignedCommenterName = request.Comment.SignedCommenterName,
            UnsignedCommenterName = request.Comment.UnsignedCommenterName,
        };

        await _commentRepository.AddAsync(entity);
        await _commentRepository.SaveChangesAsync();

        return new ServiceResult(true, "Yorum başarıyla oluşturuldu.");
    }
}
