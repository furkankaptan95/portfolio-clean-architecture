using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ServiceResult>
{
    private readonly IProjectRepository _projectRepository;
    public CreateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<ServiceResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectEntity()
        {
            Title = request.Project.Title,
            Description = request.Project.Description,
            ImageUrl = request.Project.ImageUrl,
        };

        await _projectRepository.AddAsync(entity);
        await _projectRepository.SaveChangesAsync();

        return new ServiceResult(true, "Proje başarıyla eklendi.");
    }
}
