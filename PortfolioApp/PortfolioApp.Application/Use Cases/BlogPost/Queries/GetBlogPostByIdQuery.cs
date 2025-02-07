using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Queries;
public class GetBlogPostByIdQuery : IRequest<ServiceResult<BlogPostDto>>
{
    public int Id { get; }

    public GetBlogPostByIdQuery(int id)
    {
        Id = id;
    }
}
