using MediatR;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class CreateAboutMeHandler : IRequestHandler<CreateAboutMeCommand, ServiceResult>
{
    private readonly IAboutMeRepository _aboutMeRepository;
    public CreateAboutMeHandler(IAboutMeRepository aboutMeRepository)
    {
        _aboutMeRepository = aboutMeRepository; 
    }
    public async Task<ServiceResult> Handle(CreateAboutMeCommand request, CancellationToken cancellationToken)
    {
        var entityCheck = await _aboutMeRepository.CheckAndGetAsync();

        if (entityCheck is not null)
        {
            return new ServiceResult(false, "Hakkımda bilgileri daha önceden zaten eklenmiş!");
        }

        var aboutMe = new AboutMeEntity
        {
            FullName = request.AboutMe.FullName,
            Field = request.AboutMe.Field,
            Introduction = request.AboutMe.Introduction,
            AboutMeImageUrl = request.AboutMe.AboutMeImageUrl,
            LinkedInUrl = request.AboutMe.LinkedInUrl,
            GithubUrl = request.AboutMe.GithubUrl,
            Email = request.AboutMe.Email,
            CvUrl = request.AboutMe.CvUrl,
        };

        await _aboutMeRepository.AddAsync(aboutMe);
        await _aboutMeRepository.SaveChangesAsync();

        return new ServiceResult(true,"Hakkımda bilgileri başarıyla oluşturuldu.");
    }
}
