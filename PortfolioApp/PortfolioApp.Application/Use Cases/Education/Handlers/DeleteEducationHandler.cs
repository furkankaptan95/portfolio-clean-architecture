using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class DeleteEducationHandler : IRequestHandler<DeleteEducationCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public DeleteEducationHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.Educations.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null)
        {
            return new ServiceResult(false, "Silmek istediğiniz Eğitim bilgisi bulunamadı.");
        }

        _dataDbContext.Educations.Remove(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Eğitim bilgisi başarıyla silindi.");
    }
}
