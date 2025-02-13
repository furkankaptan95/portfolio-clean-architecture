using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Queries;
public class GetBlogPostByIdWebQuery : IRequest<ServiceResult<BlogPostWebDto>>
{
    public int Id { get; }

    public GetBlogPostByIdWebQuery(int id)
    {
        Id = id;
    }
}
