using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;

namespace PortfolioApp.Application.Use_Cases.Comment.Queries;
public class GetCommentsQuery : IRequest<ServiceResult<List<CommentDto>>> { }

