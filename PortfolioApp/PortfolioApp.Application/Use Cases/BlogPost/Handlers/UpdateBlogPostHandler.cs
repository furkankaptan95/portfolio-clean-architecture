using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
public class UpdateBlogPostHandler : IRequestHandler<UpdateBlogPostCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdateBlogPostHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.BlogPost.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Blog Post bulunamadı.");
        }

        entity.Title = request.BlogPost.Title;
        entity.Content = request.BlogPost.Content;
        entity.UpdatedAt = DateTime.UtcNow;

        _dataDbContext.BlogPosts.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Blog Post başarıyla güncellendi.");
    }
}
