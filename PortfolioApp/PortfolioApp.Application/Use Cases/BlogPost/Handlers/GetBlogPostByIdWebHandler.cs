using AutoMapper;
using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.BlogPost;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class GetBlogPostByIdWebHandler : IRequestHandler<GetBlogPostByIdWebQuery, ServiceResult<BlogPostWebDto>>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;
    public GetBlogPostByIdWebHandler(IMapper mapper, IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResult<BlogPostWebDto>> Handle(GetBlogPostByIdWebQuery request, CancellationToken cancellationToken)
    {
        var blogPostEntity = await _blogPostRepository.IncludeComments(request.Id);

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
