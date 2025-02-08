using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class GetPersonalInfoHandler : IRequestHandler<GetPersonalInfoQuery, ServiceResult<PersonalInfoDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetPersonalInfoHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<PersonalInfoDto>> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.PersonalInfo.FirstOrDefaultAsync();

        if (entity is null)
        {
            return new ServiceResult<PersonalInfoDto>(false, "Kişisel bilgiler kısmına henüz bir şey eklemediniz.");
        }

        var dto = new PersonalInfoDto()
        {
            Name = entity.Name,
            Surname = entity.Surname,
            About = entity.About,
            BirthDate = entity.BirthDate,
            Adress = entity.Adress,
        };

        return new ServiceResult<PersonalInfoDto>(true, null, dto);
    }
}
