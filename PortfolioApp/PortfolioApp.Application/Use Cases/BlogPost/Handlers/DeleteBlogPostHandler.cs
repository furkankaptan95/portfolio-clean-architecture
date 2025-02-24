using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class DeleteBlogPostHandler : IRequestHandler<DeleteBlogPostCommand, ServiceResult>
{
    private readonly IBlogPostRepository _blogPostRepository;
    public DeleteBlogPostHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _blogPostRepository.GetByIdAsync(request.Id);

        if(entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Blog Post bulunamadı.");
        }

        await _blogPostRepository.DeleteAsync(entity);
        await _blogPostRepository.SaveChangesAsync();

        return new ServiceResult(true, "Blog Post başarıyla silindi.");
    }
}
