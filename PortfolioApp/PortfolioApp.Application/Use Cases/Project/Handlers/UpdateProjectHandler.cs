using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ServiceResult>
{
    private readonly IProjectRepository _projectRepository;
    public UpdateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<ServiceResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _projectRepository.GetByIdAsync(request.Project.Id);

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

        await _projectRepository.UpdateAsync(entity);
        await _projectRepository.SaveChangesAsync();

        return new ServiceResult(true, "Proje başarıyla güncellendi.");
    }
}
