using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Commands;
public class BlogPostVisibilityCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }

    public BlogPostVisibilityCommand(int id)
    {
        Id = id;
    }
}
