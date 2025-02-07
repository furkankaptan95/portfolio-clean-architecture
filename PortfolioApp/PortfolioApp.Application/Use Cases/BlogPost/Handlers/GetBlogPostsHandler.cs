using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.AboutMe.Queries;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostsHandler : IRequestHandler<GetBlogPostsQuery, ServiceResult<List<BlogPostDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetBlogPostsHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<BlogPostDto>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<BlogPostDto>();

        var entities = await _dataDbContext.BlogPosts.ToListAsync();

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
           })
           .ToList();

        return new ServiceResult<List<BlogPostDto>>(true, null, dtos);
    }
}