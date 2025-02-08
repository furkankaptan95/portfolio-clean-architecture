using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class CreatePersonalInfoHandler : IRequestHandler<CreatePersonalInfoCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreatePersonalInfoHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entityCheck = await _dataDbContext.PersonalInfo.FirstOrDefaultAsync();

        if (entityCheck is not null)
        {
            return new ServiceResult(false,"Kişisel Bilgiler daha önceden zaten eklenmiş!");
        }

        var entity = new PersonalInfoEntity()
        {
            About = request.PersonalInfo.About,
            Name = request.PersonalInfo.Name,
            Surname = request.PersonalInfo.Surname,
            BirthDate = request.PersonalInfo.BirthDate,
            Adress = request.PersonalInfo.Adress,
        };

        await _dataDbContext.PersonalInfo.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Kişisel Bilgiler başarıyla oluşturuldu.");
    }
}
