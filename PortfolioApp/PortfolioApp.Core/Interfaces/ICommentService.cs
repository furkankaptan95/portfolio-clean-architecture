using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Web.Comment;

namespace PortfolioApp.Core.Interfaces;
public interface ICommentService
{
    Task<ServiceResult> AddAsync(AddCommentDto dto);
    Task<ServiceResult<List<CommentDto>>> GetAllAsync();
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult> ApprovalAsync(int id);
}
