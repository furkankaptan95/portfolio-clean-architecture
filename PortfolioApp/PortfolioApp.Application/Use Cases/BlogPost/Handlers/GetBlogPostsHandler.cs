using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostsHandler : IRequestHandler<GetBlogPostsQuery, ServiceResult<List<BlogPostDto>>>
{    
    private readonly IBlogPostRepository _blogPostRepository;
    public GetBlogPostsHandler(IBlogPostRepository blogPostRepository)
    {        
        _blogPostRepository = blogPostRepository;
    }
    public async Task<ServiceResult<List<BlogPostDto>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<BlogPostDto>();

        var entities = await _blogPostRepository.GetAllWithComments();

        if (entities is null)
        {
            return new ServiceResult<List<BlogPostDto>>(true, "Herhangi bir Blog Post bulunmuyor.", dtos);
        }

        dtos = entities
           .Select(item => new BlogPostDto
           {
               Id = item.Id,
               Title = item.Title,
               Content = item.Content,
               PublishDate = item.PublishDate,
               IsVisible = item.IsVisible,
               UpdatedAt = item.UpdatedAt,
               ApprovedCommentsCount = item.Comments.Count,
           })
           .ToList();

        return new ServiceResult<List<BlogPostDto>>(true, null, dtos);
    }
}