using MediatR;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class CommentApprovalHandler : IRequestHandler<CommentApprovalCommand, ServiceResult>
{
    private readonly ICommentRepository _commentRepository;
    public CommentApprovalHandler(ICommentRepository commentRepository)
    {        
        _commentRepository = commentRepository;
    }
    public async Task<ServiceResult> Handle(CommentApprovalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _commentRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Yorum bulunamadı.");
        }

        entity.IsApproved = !entity.IsApproved;

        await _commentRepository.UpdateAsync(entity);
        await _commentRepository.SaveChangesAsync();

        return new ServiceResult(true, "Yorum onay durumu değiştirildi.");
    }
}
