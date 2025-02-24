using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class CommentRepository : Repository<CommentEntity, DataDbContext>, ICommentRepository
{
    public CommentRepository(DataDbContext context) : base(context)
    {

    }

    public async Task<List<CommentEntity>> GetAllWithBlogPosts()
    {
        return await _context.Comments.Include(c => c.BlogPost).ToListAsync();
    }
}
