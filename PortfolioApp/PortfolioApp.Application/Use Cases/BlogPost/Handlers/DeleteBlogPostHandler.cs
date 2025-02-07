using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class DeleteBlogPostHandler : IRequestHandler<DeleteBlogPostCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public DeleteBlogPostHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.BlogPosts.FindAsync(request.Id);

        if(entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Blog Post bulunamadı.");
        }

        _dataDbContext.BlogPosts.Remove(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Blog Post başarıyla silindi.");
    }
}
