using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Commands;
public class DeleteBlogPostCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public DeleteBlogPostCommand(int id)
    {
        Id = id;
    }
}
