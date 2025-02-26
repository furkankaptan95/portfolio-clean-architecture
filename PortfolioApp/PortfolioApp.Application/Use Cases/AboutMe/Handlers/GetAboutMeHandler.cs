using MediatR;
using PortfolioApp.Application.Use_Cases.AboutMe.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class GetAboutMeHandler : IRequestHandler<GetAboutMeQuery, ServiceResult<AboutMeDto>>
{
    private readonly IAboutMeRepository _aboutMeRepository;
    public GetAboutMeHandler(IAboutMeRepository aboutMeRepository)
    {
        _aboutMeRepository = aboutMeRepository;
    }
    public async Task<ServiceResult<AboutMeDto>> Handle(GetAboutMeQuery request, CancellationToken cancellationToken)
    {
        var aboutMeEntity = await _aboutMeRepository.CheckAndGetAsync();

        if (aboutMeEntity is null)
        {
            return new ServiceResult<AboutMeDto>(false, "Hakkımda kısmına henüz bir şey eklemediniz.");
        }

        var aboutMeDto = new AboutMeDto
        {
            FullName = aboutMeEntity.FullName,
            Field = aboutMeEntity.Field,
            Introduction = aboutMeEntity.Introduction,
            AboutMeImageUrl = aboutMeEntity.AboutMeImageUrl,
            LinkedInUrl = aboutMeEntity.LinkedInUrl,
            GithubUrl = aboutMeEntity.GithubUrl,
            Email = aboutMeEntity.Email,
            CvUrl = aboutMeEntity.CvUrl,
            InstagramUrl = aboutMeEntity.InstagramUrl,
            TwitterUrl = aboutMeEntity.TwitterUrl,
            MediumUrl = aboutMeEntity.MediumUrl,
            HeroImageUrl = aboutMeEntity.HeroImageUrl,
        };

        return new ServiceResult<AboutMeDto>(true,null,aboutMeDto);
    }
}
