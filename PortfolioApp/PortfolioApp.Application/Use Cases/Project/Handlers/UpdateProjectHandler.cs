using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdateProjectHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.Project.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Proje bilgisi bulunamadı.");
        }

        entity.Title = request.Project.Title;
        entity.Description = request.Project.Description;

        if (request.Project.ImageUrl != null)
        {
            entity.ImageUrl = request.Project.ImageUrl;
        }

        _dataDbContext.Projects.Update(entity);
        await _dataDbContext.SaveChangesAsync();

        return new ServiceResult(true, "Proje başarıyla güncellendi.");
    }
}
