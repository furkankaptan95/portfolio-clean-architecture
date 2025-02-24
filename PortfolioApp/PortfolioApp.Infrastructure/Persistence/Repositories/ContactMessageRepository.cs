using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class ContactMessageRepository : Repository<ContactMessageEntity, DataDbContext>, IContactMessageRepository
{
    public ContactMessageRepository(DataDbContext context) : base(context)
    {

    }
}
