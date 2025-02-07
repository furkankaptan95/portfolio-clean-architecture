using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Handlers;

public class CreateBlogPostHandler : IRequestHandler<CreateBlogPostCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateBlogPostHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = new BlogPostEntity
        {
            Title = request.BlogPost.Title,
            Content = request.BlogPost.Content,
        };

        await _dataDbContext.BlogPosts.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Blog Post başarıyla oluşturuldu.");
    }
}
