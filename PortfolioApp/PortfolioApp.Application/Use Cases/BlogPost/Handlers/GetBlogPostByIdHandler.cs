using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdQuery, ServiceResult<BlogPostDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetBlogPostByIdHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await _dataDbContext.BlogPosts.FindAsync(request.Id);

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
