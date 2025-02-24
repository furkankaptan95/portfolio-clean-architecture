using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class UpdateBlogPostHandler : IRequestHandler<UpdateBlogPostCommand, ServiceResult>
{
    private readonly IBlogPostRepository _blogPostRepository;
    public UpdateBlogPostHandler(IBlogPostRepository blogPostRepository)
    {      
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _blogPostRepository.GetByIdAsync(request.BlogPost.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Blog Post bulunamadı.");
        }

        entity.Title = request.BlogPost.Title;
        entity.Content = request.BlogPost.Content;
        entity.UpdatedAt = DateTime.UtcNow;

        await _blogPostRepository.UpdateAsync(entity);
        await _blogPostRepository.SaveChangesAsync();

        return new ServiceResult(true, "Blog Post başarıyla güncellendi.");
    }
}
