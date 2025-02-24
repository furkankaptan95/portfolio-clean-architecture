using MediatR;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Project.Handlers;
public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ServiceResult>
{
    private readonly IProjectRepository _projectRepository;
    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<ServiceResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _projectRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Proje bilgisi bulunamadı.");
        }

        await _projectRepository.DeleteAsync(entity);
        await _projectRepository.SaveChangesAsync();

        return new ServiceResult(true, "Proje başarıyla silindi.");
    }
}
