using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class ProjectVisibilityHandler : IRequestHandler<ProjectVisibilityCommand, ServiceResult>
{
    private readonly IProjectRepository _projectRepository;
    public ProjectVisibilityHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<ServiceResult> Handle(ProjectVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _projectRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Proje bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        await _projectRepository.UpdateAsync(entity);
        await _projectRepository.SaveChangesAsync();

        return new ServiceResult(true, "Proje görünürlüğü başarıyla güncellendi.");
    }
}
