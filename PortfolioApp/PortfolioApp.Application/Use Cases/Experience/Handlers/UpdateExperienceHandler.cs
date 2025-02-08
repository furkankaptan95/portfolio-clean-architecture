using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdateExperienceHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Experiences.FirstOrDefaultAsync(x => x.Id == request.Experience.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Güncellemek istediğiniz Deneyim bilgisi bulunamadı.");
        }

        entity.Company = request.Experience.Company;
        entity.EndDate = request.Experience.EndDate;
        entity.StartDate = request.Experience.StartDate;
        entity.Title = request.Experience.Title;
        entity.Description = request.Experience.Description;

        _dataDbContext.Experiences.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Deneyim bilgisi başarıyla güncellendi.");
    }
}
