using MediatR;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class UpdateAboutMeHandler : IRequestHandler<UpdateAboutMeCommand, ServiceResult>
{
    private readonly IAboutMeRepository _aboutMeRepository;
    public UpdateAboutMeHandler(IAboutMeRepository aboutMeRepository)
    {
        _aboutMeRepository = aboutMeRepository;
    }
    public async Task<ServiceResult> Handle(UpdateAboutMeCommand request, CancellationToken cancellationToken)
    {
        var aboutMeEntity = await _aboutMeRepository.CheckAndGetAsync();

        if (aboutMeEntity is null)
        {
            return new ServiceResult(false, "Hakkımda kısmına henüz bir şey eklemediniz.");
        }

        aboutMeEntity.Introduction = request.AboutMe.Introduction;
        aboutMeEntity.FullName = request.AboutMe.FullName;
        aboutMeEntity.Field = request.AboutMe.Field;
        aboutMeEntity.LinkedInUrl = request.AboutMe.LinkedInUrl;
        aboutMeEntity.GithubUrl = request.AboutMe.GithubUrl;
        aboutMeEntity.Email = request.AboutMe.Email;
        aboutMeEntity.TwitterUrl = request.AboutMe.TwitterUrl;
        aboutMeEntity.InstagramUrl = request.AboutMe.InstagramUrl;
        aboutMeEntity.MediumUrl = request.AboutMe.MediumUrl;

        if (request.AboutMe.CvUrl is not null)
        {
            aboutMeEntity.CvUrl = request.AboutMe.CvUrl;
        }

        if(request.AboutMe.AboutMeImageUrl is not null)
        {
            aboutMeEntity.AboutMeImageUrl = request.AboutMe.AboutMeImageUrl;
        }

        if (request.AboutMe.HeroImageUrl is not null)
        {
            aboutMeEntity.HeroImageUrl = request.AboutMe.HeroImageUrl;
        }

        await _aboutMeRepository.UpdateAsync(aboutMeEntity);
        await _aboutMeRepository.SaveChangesAsync();

        return new ServiceResult(true, "Hakkımda bilgileri başarıyla güncellendi.");
    }
}
