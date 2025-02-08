using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class ProjectVisibilityHandler : IRequestHandler<ProjectVisibilityCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public ProjectVisibilityHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(ProjectVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Proje bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        _dataDbContext.Projects.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Proje görünürlüğü başarıyla güncellendi.");
    }
}
