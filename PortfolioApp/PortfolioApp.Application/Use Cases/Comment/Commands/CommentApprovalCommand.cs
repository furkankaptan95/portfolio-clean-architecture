using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Comment.Commands;
public class CommentApprovalCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public CommentApprovalCommand(int id)
    {
        Id = id;
    }
}
