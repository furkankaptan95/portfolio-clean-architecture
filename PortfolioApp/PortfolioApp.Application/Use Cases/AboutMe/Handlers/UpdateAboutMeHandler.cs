using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class UpdateAboutMeHandler : IRequestHandler<UpdateAboutMeCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public UpdateAboutMeHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(UpdateAboutMeCommand request, CancellationToken cancellationToken)
    {
        var aboutMeEntity = await _dataDbContext.AboutMe.FirstOrDefaultAsync();

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
       
        if(request.AboutMe.CvUrl is not null)
        {
            aboutMeEntity.CvUrl = request.AboutMe.CvUrl;
        }

        if(request.AboutMe.AboutMeImageUrl is not null)
        {
            aboutMeEntity.AboutMeImageUrl = request.AboutMe.AboutMeImageUrl;
        }

        _dataDbContext.AboutMe.Update(aboutMeEntity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);
        return new ServiceResult(true, "Hakkımda bilgileri başarıyla güncellendi.");
    }
}
