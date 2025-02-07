using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class BlogPostVisibilityHandler : IRequestHandler<BlogPostVisibilityCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public BlogPostVisibilityHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(BlogPostVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Blog Post bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        _dataDbContext.BlogPosts.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Blog Post görünürlüğü başarıyla güncellendi.");
    }
}
