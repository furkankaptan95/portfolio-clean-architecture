using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Experience.Handlers;
public class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateExperienceHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExperienceEntity()
        {
            Title = request.Experience.Title,
            Company = request.Experience.Company,
            StartDate = request.Experience.StartDate,
            EndDate = request.Experience.EndDate,
            Description = request.Experience.Description,
        };

        await _dataDbContext.Experiences.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Deneyim başarıyla eklendi.");
    }
}
