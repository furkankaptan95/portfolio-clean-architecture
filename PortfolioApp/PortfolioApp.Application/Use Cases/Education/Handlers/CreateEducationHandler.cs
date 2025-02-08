using MediatR;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Education.Handlers;
public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateEducationHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
    {
        var entity = new EducationEntity()
        {
            Degree = request.Education.Degree,
            StartDate = request.Education.StartDate,
            EndDate = request.Education.EndDate,
            School = request.Education.School,
        };

        await _dataDbContext.Educations.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Eğitim başarıyla eklendi.");
    }
}
