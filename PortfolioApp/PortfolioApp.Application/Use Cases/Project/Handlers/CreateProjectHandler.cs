using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateProjectHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectEntity()
        {
            Title = request.Project.Title,
            Description = request.Project.Description,
            ImageUrl = request.Project.ImageUrl,
        };

        await _dataDbContext.Projects.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Proje başarıyla eklendi.");
    }
}
