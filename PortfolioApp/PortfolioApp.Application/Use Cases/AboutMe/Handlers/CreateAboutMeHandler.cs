using MediatR;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class CreateAboutMeHandler : IRequestHandler<CreateAboutMeCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateAboutMeHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateAboutMeCommand request, CancellationToken cancellationToken)
    {
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

        await _dataDbContext.AboutMe.AddAsync(aboutMe);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true,"Hakkımda bilgileri başarıyla oluşturuldu.");
    }
}
