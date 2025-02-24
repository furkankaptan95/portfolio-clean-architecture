using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdQuery, ServiceResult<BlogPostDto>>
{
    IBlogPostRepository _blogPostRepository;
    public GetBlogPostByIdHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(request.Id);

        if (blogPost is null)
        {
            return new ServiceResult<BlogPostDto>(false, "Blog Post bulunamadı.", null);
        }

        var dto = new BlogPostDto
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Content = blogPost.Content,
            UpdatedAt = DateTime.UtcNow,
            PublishDate = DateTime.UtcNow,
            IsVisible = blogPost.IsVisible,
        };

        return new ServiceResult<BlogPostDto>(true, null, dto);
    }
}
