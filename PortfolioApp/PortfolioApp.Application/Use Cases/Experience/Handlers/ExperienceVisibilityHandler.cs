using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class ExperienceVisibilityHandler : IRequestHandler<ExperienceVisibilityCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public ExperienceVisibilityHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(ExperienceVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Experiences.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        _dataDbContext.Experiences.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Deneyim bilgisi görünürlüğü başarıyla güncellendi.");
    }
}
