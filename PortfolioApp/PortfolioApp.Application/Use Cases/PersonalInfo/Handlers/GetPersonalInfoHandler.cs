using MediatR;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class GetPersonalInfoHandler : IRequestHandler<GetPersonalInfoQuery, ServiceResult<PersonalInfoDto>>
{
    private readonly IPersonalInfoRepository _personalInfoRepository;
    public GetPersonalInfoHandler(IPersonalInfoRepository personalInfoRepository)
    {
        _personalInfoRepository = personalInfoRepository;
    }
    public async Task<ServiceResult<PersonalInfoDto>> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _personalInfoRepository.CheckAndGetAsync();

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
