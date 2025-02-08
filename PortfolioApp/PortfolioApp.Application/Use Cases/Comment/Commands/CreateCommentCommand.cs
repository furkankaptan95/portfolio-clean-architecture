using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.Comment;

namespace PortfolioApp.Application.Use_Cases.Comment.Commands;
public class CreateCommentCommand : IRequest<ServiceResult>
{
    public AddCommentDto Comment { get; set; }
    public CreateCommentCommand(AddCommentDto comment)
    {
        Comment = comment;
    }
}
