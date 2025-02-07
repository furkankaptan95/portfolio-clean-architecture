using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
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
}
