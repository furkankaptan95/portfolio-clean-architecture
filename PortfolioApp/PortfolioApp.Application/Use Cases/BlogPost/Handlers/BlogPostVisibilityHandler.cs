using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class BlogPostVisibilityHandler : IRequestHandler<BlogPostVisibilityCommand, ServiceResult>
{
    private readonly IBlogPostRepository _blogPostRepository;
    public BlogPostVisibilityHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult> Handle(BlogPostVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _blogPostRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Blog Post bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        await _blogPostRepository.UpdateAsync(entity);
        await _blogPostRepository.SaveChangesAsync();

        return new ServiceResult(true, "Blog Post görünürlüğü başarıyla güncellendi.");
    }
}
