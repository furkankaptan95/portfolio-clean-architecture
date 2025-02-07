using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class BlogPostService : IBlogPostService
{
    private readonly IMediator _mediator;
    public BlogPostService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> AddBlogPostAsync(AddBlogPostDto dto)
    {
        var result = await _mediator.Send(new CreateBlogPostCommand(dto));

        return result;
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var result = await _mediator.Send(new BlogPostVisibilityCommand(id));

        return result;
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var result = await _mediator.Send(new DeleteBlogPostCommand(id));

        return result;
    }

    public async Task<ServiceResult<List<BlogPostDto>>> GetAllAsync()
    {
        var result = await _mediator.Send(new GetBlogPostsQuery());

        return result;
    }

    public async Task<ServiceResult<BlogPostDto>> GetByIdAsync(int id)
    {
        var result = await _mediator.Send(new GetBlogPostByIdQuery(id));

        return result;
    }

    public async Task<ServiceResult> UpdateAsync(UpdateBlogPostDto dto)
    {
        var result = await _mediator.Send(new UpdateBlogPostCommand(dto));

        return result;
    }
}
