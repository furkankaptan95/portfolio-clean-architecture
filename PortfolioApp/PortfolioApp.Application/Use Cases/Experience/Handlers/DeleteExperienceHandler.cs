using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public DeleteExperienceHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Experiences.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        _dataDbContext.Experiences.Remove(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Deneyim bilgisi başarıyla silindi.");
    }
}
