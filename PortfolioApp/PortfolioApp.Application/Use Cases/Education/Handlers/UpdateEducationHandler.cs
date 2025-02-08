using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class UpdateEducationHandler : IRequestHandler<UpdateEducationCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdateEducationHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Educations.FirstOrDefaultAsync(x => x.Id == request.Education.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        entity.School = request.Education.School;
        entity.EndDate = request.Education.EndDate;
        entity.StartDate = request.Education.StartDate;
        entity.Degree = request.Education.Degree;

        _dataDbContext.Educations.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Eğitim bilgisi başarıyla güncellendi.");
    }
}
