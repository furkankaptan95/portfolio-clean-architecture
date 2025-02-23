using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class BlogPostRepository : Repository<BlogPostEntity, DataDbContext>, IBlogPostRepository
{
    public BlogPostRepository(DataDbContext context) : base(context)
    {
        
    }
}
