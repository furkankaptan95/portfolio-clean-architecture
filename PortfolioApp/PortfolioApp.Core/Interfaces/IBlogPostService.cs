using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Core.Interfaces;
public interface IBlogPostService
{
    Task<ServiceResult> AddBlogPostAsync(AddBlogPostDto dto);
}
