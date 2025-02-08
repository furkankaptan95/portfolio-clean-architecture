using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.Comment;

namespace PortfolioApp.Core.Interfaces;
public interface ICommentService
{
    Task<ServiceResult> AddAsync(AddCommentDto dto);
    Task<ServiceResult> DeleteAsync(int id);
}
