using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface IBlogPostRepository : IRepository<BlogPostEntity>
{
    Task<BlogPostEntity> IncludeComments(int id);
    Task<List<BlogPostEntity>> GetAllWithComments();
}
