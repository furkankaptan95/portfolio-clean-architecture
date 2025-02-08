using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class EducationVisibilityHandler : IRequestHandler<EducationVisibilityCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public EducationVisibilityHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(EducationVisibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Educations.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Görünürlüğünü güncellemek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        entity.IsVisible = !entity.IsVisible;

        _dataDbContext.Educations.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Eğitim bilgisi görünürlüğü başarıyla güncellendi.");
    }
}
