using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.BlogPost;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostByIdWebHandler : IRequestHandler<GetBlogPostByIdWebQuery, ServiceResult<BlogPostWebDto>>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;
    public GetBlogPostByIdWebHandler(DataDbContext dataDbContext, IMapper mapper )
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }
    public async Task<ServiceResult<BlogPostWebDto>> Handle(GetBlogPostByIdWebQuery request, CancellationToken cancellationToken)
    {
        var blogPostEntity = await _dataDbContext.BlogPosts.Include(bp=>bp.Comments).FirstOrDefaultAsync(bp=>bp.Id == request.Id);

        if (blogPostEntity is null)
        {
            return new ServiceResult<BlogPostWebDto>(false, "Blog Post bulunamadı.", null);
        }

        var blogPostDto = new BlogPostWebDto
        {
            Id = blogPostEntity.Id,
            Title = blogPostEntity.Title,
            Content = blogPostEntity.Content,
            PublishDate = blogPostEntity.PublishDate,
        };

        var commentEntities = blogPostEntity.Comments;

        var commentDtos = _mapper.Map<List<CommentWebDto>>(commentEntities);

        blogPostDto.Comments = commentDtos;

        return new ServiceResult<BlogPostWebDto>(true, null, blogPostDto);
    }
}
