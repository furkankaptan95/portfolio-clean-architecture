using MediatR;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
public class UpdatePersonalInfoHandler : IRequestHandler<UpdatePersonalInfoCommand, ServiceResult>
{
    private readonly IPersonalInfoRepository _personalInfoRepository;
    public UpdatePersonalInfoHandler(IPersonalInfoRepository personalInfoRepository)
    {
        _personalInfoRepository = personalInfoRepository;
    }
    public async Task<ServiceResult> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _personalInfoRepository.CheckAndGetAsync();

        if (entity is null)
        {
            return new ServiceResult(false, "Kişisel bilgiler kısmına henüz bir şey eklemediniz.");
        }

        entity.Name = request.PersonalInfo.Name;
        entity.Surname = request.PersonalInfo.Surname;
        entity.About = request.PersonalInfo.About;
        entity.BirthDate = request.PersonalInfo.BirthDate;
        entity.Adress = request.PersonalInfo.Adress;

        await _personalInfoRepository.UpdateAsync(entity);
        await _personalInfoRepository.SaveChangesAsync();

        return new ServiceResult(true, "Kişisel bilgiler başarıyla güncellendi.");
    }
}
