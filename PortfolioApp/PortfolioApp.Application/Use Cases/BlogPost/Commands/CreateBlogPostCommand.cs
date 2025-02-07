using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Commands;
public class CreateBlogPostCommand : IRequest<ServiceResult>
{
    public AddBlogPostDto BlogPost { get; set; }
    public CreateBlogPostCommand(AddBlogPostDto dto)
    {
        BlogPost = dto;
    }
}
