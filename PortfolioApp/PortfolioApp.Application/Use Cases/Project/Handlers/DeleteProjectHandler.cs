using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public DeleteProjectHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Proje bilgisi bulunamadı.");
        }

        _dataDbContext.Projects.Remove(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Proje başarıyla silindi.");
    }
}
