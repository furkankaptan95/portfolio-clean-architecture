using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class BlogPostRepository : Repository<BlogPostEntity, DataDbContext>, IBlogPostRepository
{
    public BlogPostRepository(DataDbContext context) : base(context)
    {
        
    }

    public async Task<List<BlogPostEntity>> GetAllWithComments()
    {
        return await _context.BlogPosts.Include(bp => bp.Comments.Where(c=>c.IsApproved)).ToListAsync();
    }

    public async Task<BlogPostEntity> IncludeComments(int id)
    {
        return await _context.BlogPosts.Where(bp=>bp.IsVisible).Include(bp => bp.Comments.Where(c=>c.IsApproved)).FirstOrDefaultAsync(bp=>bp.Id == id);
    }
}
