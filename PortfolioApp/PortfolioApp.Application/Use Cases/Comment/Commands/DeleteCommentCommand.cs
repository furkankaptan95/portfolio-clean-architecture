using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Comment.Commands;
public class DeleteCommentCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public DeleteCommentCommand(int id)
    {
        Id = id;
    }
}
