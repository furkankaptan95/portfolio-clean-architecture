using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;

public class CreateBlogPostHandler : IRequestHandler<CreateBlogPostCommand, ServiceResult>
{
    private readonly IBlogPostRepository _blogPostRepository;
    public CreateBlogPostHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = new BlogPostEntity
        {
            Title = request.BlogPost.Title,
            Content = request.BlogPost.Content,
        };

        await _blogPostRepository.AddAsync(entity);
        await _blogPostRepository.SaveChangesAsync();

        return new ServiceResult(true, "Blog Post başarıyla oluşturuldu.");
    }
}
