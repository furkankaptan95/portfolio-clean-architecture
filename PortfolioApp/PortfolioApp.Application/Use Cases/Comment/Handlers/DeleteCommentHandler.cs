using MediatR;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, ServiceResult>
{
    private readonly ICommentRepository _commentRepository;
    public DeleteCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<ServiceResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _commentRepository.GetByIdAsync(request.Id); 
        
        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz yorum bulunamadı.");
        }

        await _commentRepository.DeleteAsync(entity);
        await _commentRepository.SaveChangesAsync();

        return new ServiceResult(true, "Yorum başarıyla silindi.");
    }
}
