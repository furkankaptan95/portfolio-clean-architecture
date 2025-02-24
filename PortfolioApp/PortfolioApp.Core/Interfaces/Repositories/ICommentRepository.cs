using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.Interfaces.Repositories;
public interface ICommentRepository : IRepository<CommentEntity>
{
    Task<List<CommentEntity>> GetAllWithBlogPosts();
}
