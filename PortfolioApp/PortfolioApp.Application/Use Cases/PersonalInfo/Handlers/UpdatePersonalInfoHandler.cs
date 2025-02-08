using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class UpdatePersonalInfoHandler : IRequestHandler<UpdatePersonalInfoCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdatePersonalInfoHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.PersonalInfo.FirstOrDefaultAsync();

        if (entity is null)
        {
            return new ServiceResult(false, "Kişisel bilgiler kısmına henüz bir şey eklemediniz.");
        }

        entity.Name = request.PersonalInfo.Name;
        entity.Surname = request.PersonalInfo.Surname;
        entity.About = request.PersonalInfo.About;
        entity.BirthDate = request.PersonalInfo.BirthDate;
        entity.Adress = request.PersonalInfo.Adress;

        _dataDbContext.PersonalInfo.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Kişisel bilgiler başarıyla güncellendi.");
    }
}
