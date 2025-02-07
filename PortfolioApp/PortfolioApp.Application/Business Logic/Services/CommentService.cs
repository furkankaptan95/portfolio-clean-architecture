using MediatR;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class CommentService : ICommentService
{
    private readonly IMediator _mediator;
    public CommentService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> AddAsync(AddCommentDto dto)
    {
        var result = await _mediator.Send(new CreateCommentCommand(dto));

        return result;
    }
}
