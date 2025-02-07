using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Commands;
public class UpdateBlogPostCommand : IRequest<ServiceResult>
{
    public UpdateBlogPostDto BlogPost { get; set; }
    public UpdateBlogPostCommand(UpdateBlogPostDto blogPost)
    {
        BlogPost = blogPost;
    }
}
