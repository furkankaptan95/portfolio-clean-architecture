using MediatR;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class CreatePersonalInfoHandler : IRequestHandler<CreatePersonalInfoCommand, ServiceResult>
{
    private readonly IPersonalInfoRepository _personalInfoRepository;
    public CreatePersonalInfoHandler(IPersonalInfoRepository personalInfoRepository)
    {
        _personalInfoRepository = personalInfoRepository;
    }
    public async Task<ServiceResult> Handle(CreatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entityCheck = await _personalInfoRepository.CheckAndGetAsync();

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

        await _personalInfoRepository.AddAsync(entity);
        await _personalInfoRepository.SaveChangesAsync();

        return new ServiceResult(true, "Kişisel Bilgiler başarıyla oluşturuldu.");
    }
}
